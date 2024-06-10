using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bombにのみスクリプトがあると、別オブジェクトで参照できないので管理スクリプトを用意
/// Bombだとすぐに自身が消えてしまうので、敵の弾を削除しきれない
/// </summary>
public class BombManager : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject scoreText;
    
    private Controller controllerScript;
    private EnemyBulletManager enemyBulletManager;
    private ScoreManager scoreManager;
    private float timer = 0;
    private bool isTimerStart = false;

    // 発射位置調整用定数
    const float OFFSET_Y = 1.0f;
    // 爆発までの時間
    const float EXPLOSION_START = 1.5f;
    // 爆発終了時間
    const float EXPLOSION_END = 2.0f;
    // ランダム生成の弾を爆弾で壊したときの加算スコア
    const int RANDOM_BULLET_BREAK_SCORE = 10;
    // Playerに向かって飛ぶ弾を爆弾で壊したときの加算スコア
    const int AIMED_BULLET_BREAK_SCORE = 30;
    // ホーミング弾を爆弾で壊したときの加算スコア
    const int CHASE_BULLET_BREAK_SCORE = 50;

    void Start()
    {
        controllerScript = GetComponent<Controller>();
        enemyBulletManager = enemy.GetComponent<EnemyBulletManager>();
        scoreManager = scoreText.GetComponent<ScoreManager>();
    }

    void Update()
    {
        if(controllerScript.IsBombInstantiate)
        {
            // 位置調整(この場合のtransform.positionはPlayer)
            Vector3 Pos = new Vector3(transform.position.x + OFFSET_Y, transform.position.y, transform.position.z);

            // 生成
            Instantiate(bombPrefab, Pos, Quaternion.identity);

            controllerScript.IsBombInstantiate = false;

            isTimerStart = true;
        }

        // 爆発
        if (isTimerStart)
        {
            timer += Time.deltaTime;
            // 0.5秒間、敵の弾削除
            if(timer >= EXPLOSION_START)
            {
                // 消した弾に応じてスコア加算
                int DestroyedCount = enemyBulletManager.DestroyAllBullets("Random");
                scoreManager.AddScore(DestroyedCount * RANDOM_BULLET_BREAK_SCORE);

                DestroyedCount = enemyBulletManager.DestroyAllBullets("Aimed");
                scoreManager.AddScore(DestroyedCount * AIMED_BULLET_BREAK_SCORE);

                DestroyedCount = enemyBulletManager.DestroyAllBullets("Chase");
                scoreManager.AddScore(DestroyedCount * CHASE_BULLET_BREAK_SCORE);
            }
            // リセット
            if(timer >= EXPLOSION_END)
            {
                timer = 0;
                isTimerStart = false;
            }
        }
    }
}
