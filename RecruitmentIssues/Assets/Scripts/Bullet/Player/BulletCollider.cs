using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : BulletBase
{
    [SerializeField] private ScoreManager scoreManagerScript;

    // ランダムにばらまかれる弾の点数
    const int RANDOM_BULLET_SCORE = 3;
    // Playerに向かって飛ぶ弾の点数
    //const int AIMED_BULLET_SCORE = 3; 
    // 追跡する弾の点数
    //const int CHASE_BULLET_SCORE = 3;

    void Start()
    {
        scoreManagerScript = GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemyと接触時、自身を削除
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }

        // Score加算 --------------------------
        // ランダム
        if (collision.CompareTag("RandomBullet"))
        {
            Destroy(this.gameObject);

            // スコア加算
            scoreManagerScript.AddScore(RANDOM_BULLET_SCORE);
        }
        //// Playerに向かっていく
        //else if (collision.CompareTag("AimedBullet"))
        //{
        //    Destroy(this.gameObject);

        //    // スコア加算
        //    scoreManagerScript.AddScore(AIMED_BULLET_SCORE);
        //}
        //// 追跡
        //else if (collision.CompareTag("ChaseBullet"))
        //{
        //    Destroy(this.gameObject);

        //    scoreManagerScript.AddScore(CHASE_BULLET_SCORE);
        //}
    }
}
