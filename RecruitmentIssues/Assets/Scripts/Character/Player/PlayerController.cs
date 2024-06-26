using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterBase
{
    // 弾Prefab取得
    [SerializeField] private GameObject bulletPrefab;
    // 爆発エフェクトPrefab取得
    [SerializeField] private GameObject explosionPrefab;
    // リロードテキスト取得
    [SerializeField] private GameObject reloadText;
    // 残弾数テキスト取得
    [SerializeField] private GameObject currentBulletText;
    // スコアスクリプト取得
    [SerializeField] private ScoreManager scoreManager;
    // 残機表示用UIスクリプト取得
    [SerializeField] private LifeStarSpawner lifeStarsSpawner;

    private SpriteRenderer spriteRenderer;
    private PlayerCollider playerCollider;

    // 残り機数
    private int life = 3;
    // 現在の弾数
    private int currentBullet = 5;
    // 現在の爆弾数
    private int currentBomb = 3;

    // リロードタイマー
    private float reloadTimer = 0.0f;

    // 減速フラグ
    private bool isLow = false;
    // 爆弾生成フラグ
    private bool isBombInstantiate = false;
    // リロード中かどうかを表すフラグ
    private bool isReloading = false;

    // キー入力を受け取って保存する用
    private Vector2 input = Vector2.zero;

    // 弾発射位置調整用定数
    const float BULLET_OFFSET_X = 1.0f;
    // 弾発射位置調整用定数   二発発射時の弾間隔/2
    const float BULLET_OFFSET_Y = 0.2f;
    // 移動速度原則倍率定数
    const float SLOWDOWN_FACTOR = 0.5f;
    // リロード時間
    const float RELOAD_TIME = 1.5f;
    // 最大弾数
    const int MAX_BULLET = 10;
    // 最大爆弾数
    const int MAX_BOMB = 3;
    // 二発発射が可能になるスコア
    const int DOUBLE_BULLET_SCORE = 4000;

    public int CurrentBomb { get => currentBomb; }
    public bool IsBombInstantiate { get => isBombInstantiate; set => isBombInstantiate = value; }
    public Vector2 Input { get => input; }
    public int CurrentBullet { get => currentBullet; }

    override protected void Start()
    {
        // 基底クラスのStart呼び出し
        base.Start();

        playerCollider = GetComponent<PlayerCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        reloadTimer = RELOAD_TIME; 
        currentBullet = MAX_BULLET;
        currentBomb = MAX_BOMB;

        isLow = false;
        isBombInstantiate = false;
        isReloading = false;

        reloadText.SetActive(false);
    }

    override protected void Update()
    {
        // 基底クラスのUpdate呼び出し
        base.Update();

        // 被ダメージ
        if (playerCollider.IsDamage)
        {
            life--;
            playerCollider.IsDamage = false;
            isBlink = true;
            // 残機UI更新
            lifeStarsSpawner.UpdateLifeStarsUI(life);

        }

        // リロード
        if (currentBullet <= 0 || isReloading)
        {
            reload();
        }

        // 残機確認
        checkLife();
    }

    // 移動関数
    private void move()
    {
        if(input.x != 0 || input.y != 0)
        {
            if (isLow)
            {
                // 減速時
                transform.Translate(input * moveSpeed * SLOWDOWN_FACTOR * Time.deltaTime);
            }
            else
            {
                // 入力された方向に移動
                transform.Translate(input * moveSpeed * Time.deltaTime);
            }
        }
    }

    // 攻撃関数
    private void fire()
    {
        // ある程度のスコアを超えたら
        if(scoreManager.Score >= DOUBLE_BULLET_SCORE)
        {
            // 二発減らす
            currentBullet -= 2;
            // 二発同時発射
            bulletPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_X, transform.position.y + BULLET_OFFSET_Y, transform.position.z);
            Instantiate(bulletPrefab);
            bulletPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_X, transform.position.y - BULLET_OFFSET_Y, transform.position.z);
            Instantiate(bulletPrefab);
        }
        else
        {
            currentBullet--;

            // 位置調整
            bulletPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_Y, transform.position.y, transform.position.z);

            // 生成
            Instantiate(bulletPrefab);
        }

    }

    // 爆撃関数
    private void bomb()
    {
        currentBomb--;
        isBombInstantiate = true;
    }

    // 残機確認
    private void checkLife()
    {
        if (life > 0)
        {
            // 移動可能時のみ移動
            if (playerCollider.IsMoveAble)
            {
                move();
            }
        }
        else
        {
            // リロードテキスト非表示
            reloadText.SetActive(false);

            // 自身を非表示
            this.gameObject.SetActive(false);

            // 爆発アニメーション終了フラグを受け取ったら次に進む
            explosionEffect();

        }
    }

    // エフェクト生成
    private void explosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    // 点滅処理
    override protected void blinking()
    {
        // 内部時刻を経過させる
        timer += Time.deltaTime;

        // timer〜blinkIntervalの間で繰り返し値を取得
        float repeatValue = Mathf.Repeat((float)timer, blinkInterval);

        // インターバルの半分以上の時は表示
        if (repeatValue >= blinkInterval * 0.5f)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    // リロード関数
    private void reload()
    {
        // 残弾数テキストを非表示
        currentBulletText.SetActive(false);
        // リロード中テキストを表示
        reloadText.SetActive(true);

        reloadTimer -= Time.deltaTime;
        if(reloadTimer <= 0)
        {
            // リセット -----------------------------
            reloadText.SetActive(false);

            // 二発発射の時、最大弾数も二倍
            if (scoreManager.Score >= DOUBLE_BULLET_SCORE)
            {
                currentBullet = MAX_BULLET * 2;
            }
            else
            {
                currentBullet = MAX_BULLET;
            }
            reloadTimer = RELOAD_TIME;
            currentBulletText.SetActive(true);
            isReloading = false;
        }

    }

    // 以下キー入力判定関数　================================================

    // 移動
    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();

        // Z軸で-90度回転しているので、正しく移動するように値を変換
        input = new Vector2(-rawInput.y, rawInput.x);
    }

    // 攻撃
    public void OnFireEvent(InputAction.CallbackContext context)
    {
        // 左クリック or pad右トリガー を押したら
        if (context.phase == InputActionPhase.Performed && currentBullet > 0 && life > 0 && !isReloading)
        {
            fire();
        }
    }

    // 低速
    public void OnLowEvent(InputAction.CallbackContext context)
    {
        // 左Shift or pad左トリガー を押したら
        if (context.phase == InputActionPhase.Performed)
        {
            isLow = true;
        }
        // キーを離したら
        else if (context.phase == InputActionPhase.Canceled)
        {
            isLow = false;
        }
    }

    // 爆撃
    public void OnBombEvent(InputAction.CallbackContext context)
    {
        // Space or pad右上トリガー を押したら  爆弾が残っているとき
        if (context.phase == InputActionPhase.Performed && currentBomb > 0 && life > 0)
        {
            bomb();
        }
    }

    // リロード
    public void OnReloadEvent(InputAction.CallbackContext context)
    {
        // R or pad左ボタン を押したら
        if (context.phase == InputActionPhase.Performed && !isReloading)
        {
            isReloading = true;
        }
    }
}
