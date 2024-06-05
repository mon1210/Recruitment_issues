using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    // 弾Prefab取得
    [SerializeField] private GameObject bulletPrefab;
    // 爆発エフェクトPrefab取得
    [SerializeField] private GameObject explosionPrefab;
    // 残機表示用UIオブジェクト取得
    [SerializeField] private LifeStarSpawner lifeStarsSpawner;
    // 移動スピード
    [SerializeField] private int moveSpeed = 0;
    // 点滅周期
    [SerializeField] private float blinkInterval = 0.0f;

    // キー入力を受け取って保存する用
    private Vector2 input = Vector2.zero;

    private Collider colliderScript;
    private SpriteRenderer spriteRenderer;
    // 減速フラグ
    private bool isLow = false;
    // 点滅フラグ
    private bool isBlink = false;
    // 爆弾生成フラグ
    private bool isBombInstantiate = false;
    // 残り機数
    private int life = 3;
    // 現在の弾数
    private int currentBullet = 5;
    // 現在の爆弾数
    private int currentBomb = 3;

    private float timer = 0.0f;
    private float blinkTimer = 0.0f;
    private float reloadTimer = 0.0f;

    // 弾発射位置調整用定数
    const float BULLET_OFFSET_Y = 1.0f;
    // 移動速度原則倍率定数
    const float SLOWDOWN_FACTOR = 0.5f;
    // 点滅時間
    const float BLINK_TIME = 1.0f;
    // 装填時間
    const float RELOAD_TIME = 1.5f;
    // 最大弾数
    const int MAX_BULLET = 5;
    // 最大爆弾数
    const int MAX_BOMB = 3;

    public Vector2 Input { get => input;}
    public int Life { get => life;}
    public bool IsBombInstantiate { get => isBombInstantiate; set => isBombInstantiate = value; }
    public int CurrentBomb { get => currentBomb;}

    void Start()
    {
        colliderScript = GetComponent<Collider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        blinkTimer = BLINK_TIME;
        reloadTimer = RELOAD_TIME; 
        currentBullet = MAX_BULLET;
        currentBomb = MAX_BOMB;
    }

    void Update()
    {
        // 被ダメージ
        if(colliderScript.IsDamage)
        {
            life--;
            colliderScript.IsDamage = false;
            isBlink = true;
            // 残機UI更新
            lifeStarsSpawner.UpdateLifeStarsUI(life);

        }

        // リロード
        if (currentBullet <= 0)
        {
            reload();
        }

        // 点滅処理管理
        manageBlinking();

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
        currentBullet--;

        // 位置調整
        bulletPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_Y, transform.position.y, transform.position.z);
        
        // 生成
        Instantiate(bulletPrefab);
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
            if (colliderScript.IsMoveAble)
            {
                move();
            }
        }
        else
        {
            // 自身を非表示
            this.gameObject.SetActive(false);

            // 爆発アニメーション終了フラグを受け取ったら次に進む
            explosionEffect();

        }
    }

    // エフェクト生成
    private void explosionEffect()
    {
        explosionPrefab.transform.position = transform.position;

        Instantiate(explosionPrefab);
    }

    // 点滅処理
    private void blinking()
    {
        // 内部時刻を経過させる
        timer += Time.deltaTime;

        // timer～blinkIntervalの間で繰り返し値を取得
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

    // 点滅が指定した秒間続くようにする
    private void manageBlinking()
    {
        if (isBlink)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer > 0)
            {
                blinking();
            }
            else
            {
                // リセット
                blinkTimer = BLINK_TIME;
                isBlink = false;
            }
        }
    }

    // リロード関数
    private void reload()
    {
        reloadTimer -= Time.deltaTime;
        if(reloadTimer <= 0)
        {
            // リセット
            currentBullet = MAX_BULLET;
            reloadTimer = RELOAD_TIME;
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
        if (context.phase == InputActionPhase.Performed && currentBullet > 0)
        {
            fire();
        }
    }

    // 低速
    public void OnLowEvent(InputAction.CallbackContext context)
    {
        // 左クリック or pad右トリガー を押したら
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
        // 左クリック or pad右トリガー を押したら  爆弾が残っているとき
        if (context.phase == InputActionPhase.Performed && currentBomb > 0)
        {
            bomb();
        }
    }
}
