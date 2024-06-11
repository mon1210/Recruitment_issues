using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : CharacterBase
{
    private EnemyCollider enemyCollider;
    private EnemyBulletManager enemyBulletManager;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject randomBulletPrefab;
    [SerializeField] private GameObject aimedBulletPrefab;
    [SerializeField] private GameObject chaseBulletPrefab;
    // 爆発エフェクトPrefab取得
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private GameManager gameManager;
    // 残り体力
    [SerializeField] private int hitPoint = 100;

    private float moveTimer = 0.0f;
    private bool isStartAimedBullet = false;
    private bool isStartChaseBullet = false;

    // 移動判定までのタイム
    const float REVERSE_MOVE_TIME = 3.0f;
    // ランダムな弾の発射間隔定数
    const float RANDOM_FIRE_INTERVAL = 0.25f;
    // Playerに向かう弾の発射間隔定数
    const float AIMED_FIRE_INTERVAL = 2.0f;
    // ホーミング弾の発射間隔定数
    const float CHASE_FIRE_INTERVAL = 5.0f;
    // 弾発射位置調整用定数
    const float BULLET_OFFSET_X = 1.0f;

    override protected void Start()
    {
        // 基底クラスのStart呼び出し
        base.Start();

        enemyCollider = GetComponent<EnemyCollider>();
        enemyBulletManager = GetComponent<EnemyBulletManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ランダムな弾の生成を開始
        StartCoroutine(SpawnRandomBullet());
    }

    override protected void Update()
    {
        // 基底クラスのUpdate呼び出し
        base.Update();

        // 点滅以外で赤になっていた場合、色修正
        if (!isBlink && spriteRenderer.color == Color.red)
        {
            spriteRenderer.color = Color.white;
        }

        // 被ダメージ
        if (enemyCollider.IsDamage)
        {
            hitPoint--;
            enemyCollider.IsDamage = false;
            isBlink = true;
        }

        if (hitPoint > 0)
        {
            // 移動可能時のみ移動
            if (enemyCollider.IsMoveAble)
            {
                move();
            }
        }
        else
        {
            // 自身を非表示
            this.gameObject.SetActive(false);

            // 爆発エフェクト表示
            explosionEffect();
        }

        // Playerに向かって飛ぶ弾の生成開始
        if (gameManager.IsAimedStart && !isStartAimedBullet)
        {
            StartCoroutine(SpawnAimedBullet());
            gameManager.IsAimedStart = false;
            isStartAimedBullet = true;
        }
        // ホーミング弾の生成開始
        if (gameManager.IsChaseStart && !isStartChaseBullet)
        {
            StartCoroutine(SpawnChaseBullet());
            gameManager.IsChaseStart = false;
            isStartChaseBullet = true;
        }
    }

    // エフェクト生成
    private void explosionEffect()
    {
        Vector3 defaultSize = explosionPrefab.transform.localScale;

        // サイズ変更
        explosionPrefab.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);

        // 生成
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // ドラゴンでも使いまわすので元に戻す
        explosionPrefab.transform.localScale = defaultSize;
    }

    // 移動関数
    private void move()
    {
        moveTimer += Time.deltaTime;

        // 最初は上移動
        if (moveTimer >= REVERSE_MOVE_TIME)
        {
            // 移動方向反転
            moveSpeed *= -1.0f;
            moveTimer = 0.0f;
        }

        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    // 点滅処理
    override protected void blinking()
    {
        // 内部時刻を経過させる
        timer += Time.deltaTime;

        // timer～blinkIntervalの間で繰り返し値を取得
        float repeatValue = Mathf.Repeat((float)timer, blinkInterval);

        // インターバルの半分以上の時は赤色
        if (repeatValue >= blinkInterval * 0.5f)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }

    // ランダムな弾を生成する
    private IEnumerator SpawnRandomBullet()
    {
        while (true) 
        { 
            Vector3 Pos = new Vector3(transform.position.x - BULLET_OFFSET_X, transform.position.y, transform.position.z);
            
            // 生成
            GameObject RandomBullet = Instantiate(randomBulletPrefab, Pos, Quaternion.identity);
            
            // リストに追加
            enemyBulletManager.AddBulletList("Random", RandomBullet);

            // インターバルを待つ
            yield return new WaitForSeconds(RANDOM_FIRE_INTERVAL);
        }
    }

    // Playerに向かって飛ぶ弾を生成する
    private IEnumerator SpawnAimedBullet()
    {
        while (true)
        {
            Vector3 Pos = new Vector3(transform.position.x - BULLET_OFFSET_X, transform.position.y, transform.position.z);

            // 生成
            GameObject AimedBullet = Instantiate(aimedBulletPrefab, Pos, Quaternion.identity);

            // リストに追加
            enemyBulletManager.AddBulletList("Aimed", AimedBullet);

            // インターバルを待つ
            yield return new WaitForSeconds(AIMED_FIRE_INTERVAL);
        }
    }

    // ホーミング弾を生成する
    private IEnumerator SpawnChaseBullet()
    {
        while (true)
        {
            Vector3 Pos = new Vector3(transform.position.x - BULLET_OFFSET_X, transform.position.y, transform.position.z);

            // 生成
            GameObject ChaseBullet = Instantiate(chaseBulletPrefab, Pos, Quaternion.identity);

            // リストに追加
            enemyBulletManager.AddBulletList("Chase", ChaseBullet);

            // インターバルを待つ
            yield return new WaitForSeconds(CHASE_FIRE_INTERVAL);
        }
    }
}
