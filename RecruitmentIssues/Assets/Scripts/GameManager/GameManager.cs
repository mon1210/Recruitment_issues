using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Player取得
    [SerializeField] private GameObject player;
    // Enemy取得
    [SerializeField] private GameObject enemy;
    // ドラゴン頭(上)取得
    [SerializeField] private GameObject dragon;
    // ドラゴン頭(下)取得
    [SerializeField] private GameObject dragonBottom;

    private EnemyController enemyControllerScript;
    private EnemyBulletManager enemyBulletManager;

    // 終了タイマー
    private float endTimer = 0.0f;

    // Playerに向かって飛ぶ弾生成開始フラグ
    private bool isAimedStart = false;
    // ホーミング弾生成開始フラグ
    private bool isChaseStart = false;

    // PlayerかEnemyが死亡してから終了するまでの時間(死亡時の爆発アニメーションなどを考慮)
    const float END_TIME = 5.0f;

    public bool IsAimedStart { get => isAimedStart; set => isAimedStart = value; }
    public bool IsChaseStart { get => isChaseStart; set => isChaseStart = value; }

    void Start()
    {
        enemyControllerScript = enemy.GetComponent<EnemyController>();
        enemyBulletManager = enemy.GetComponent<EnemyBulletManager>();

        endTimer = 0.0f;
        isAimedStart = false;
        isChaseStart= false;
    }

    void Update()
    {
        // Player死亡時
        if(!player.activeInHierarchy)
        {
            // Enemyのコールチン(弾生成)終了
            enemyControllerScript.StopAllCoroutines();

            // ゲーム終了
            GameEnd();
        }

        // Enemy死亡時
        if (!enemy.activeInHierarchy)
        {
            // ゲーム終了
            GameEnd();
        }


        // ドラゴン二体死亡、ホーミング弾開始
        if (!dragon.activeInHierarchy && !dragonBottom.activeInHierarchy)
        {
            isChaseStart = true;
        }
        // ドラゴン一体死亡、Playerに向かう弾開始（else if にすることで二体目死亡後にもう一度通ることを回避）
        else if (!dragon.activeInHierarchy || !dragonBottom.activeInHierarchy)
        {
            isAimedStart = true;
        }

    }

    private void GameEnd()
    {
        // 敵の弾削除
        enemyBulletManager.DestroyAllBullets(EnemyBulletManager.BulletKind.Random);
        enemyBulletManager.DestroyAllBullets(EnemyBulletManager.BulletKind.Random);
        enemyBulletManager.DestroyAllBullets(EnemyBulletManager.BulletKind.Random);

        // 5秒後にGameOverSceneへ遷移
        endTimer += Time.deltaTime;
        if (endTimer >= END_TIME)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
