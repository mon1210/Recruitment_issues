using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : BulletBase
{
    private ScoreManager scoreManager;

    // ランダムにばらまかれる弾の点数
    const int RANDOM_BULLET_SCORE = 3;
    // Playerに向かって飛ぶ弾の点数
    const int AIMED_BULLET_SCORE = 5; 
    // 追跡する弾の点数
    const int CHASE_BULLET_SCORE = 10;

    void Start()
    {
        // PlayerBulletがPrefabのため、Findを使用
        scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemyと接触時、自身を削除
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }

        // 敵の弾と接触、Score加算 ----------------------------------
        // ランダム
        if (collision.CompareTag("RandomBullet"))
        {
            Destroy(this.gameObject);

            // スコア加算
            scoreManager.AddScore(RANDOM_BULLET_SCORE);
        }
        // Playerに向かっていく
        else if (collision.CompareTag("AimedBullet"))
        {
            Destroy(this.gameObject);

            // スコア加算
            scoreManager.AddScore(AIMED_BULLET_SCORE);
        }
        // 追跡
        else if (collision.CompareTag("ChaseBullet"))
        {
            Destroy(this.gameObject);

            scoreManager.AddScore(CHASE_BULLET_SCORE);
        }
    }
}
