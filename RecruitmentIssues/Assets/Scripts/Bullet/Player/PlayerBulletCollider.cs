using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollider : MonoBehaviour
{
    private ScoreManager scoreManager;

    // 敵の点数
    const int ENEMY_SCORE = 5;
    // ランダムにばらまかれる弾の点数
    const int RANDOM_BULLET_SCORE = 3;
    // Playerに向かって飛ぶ弾の点数
    const int AIMED_BULLET_SCORE = 5; 
    // ホーミング弾の点数
    const int CHASE_BULLET_SCORE = 10;

    void Start()
    {
        // PlayerBulletがPrefabのため、Findを使用
        scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }

    // 接触時に自身を削除して、スコアを加算する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);

            // スコア加算
            scoreManager.AddScore(ENEMY_SCORE);
        }

        // 敵の弾 -------------------------------------
        // ランダム
        if (collision.CompareTag("RandomBullet"))
        {
            Destroy(this.gameObject);

            // スコア加算
            scoreManager.AddScore(RANDOM_BULLET_SCORE);
        }
        // Playerに向かって飛ぶ
        else if (collision.CompareTag("AimedBullet"))
        {
            Destroy(this.gameObject);

            // スコア加算
            scoreManager.AddScore(AIMED_BULLET_SCORE);
        }
        // ホーミング
        else if (collision.CompareTag("ChaseBullet"))
        {
            Destroy(this.gameObject);

            // スコア加算
            scoreManager.AddScore(CHASE_BULLET_SCORE);
        }
    }
}
