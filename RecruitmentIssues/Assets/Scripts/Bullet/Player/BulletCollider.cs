using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : BulletBase
{
    [SerializeField] private ScoreManager scoreManagerScript;

    // �����_���ɂ΂�܂����e�̓_��
    const int RANDOM_BULLET_SCORE = 3;
    // Player�Ɍ������Ĕ�Ԓe�̓_��
    //const int AIMED_BULLET_SCORE = 3; 
    // �ǐՂ���e�̓_��
    //const int CHASE_BULLET_SCORE = 3;

    void Start()
    {
        scoreManagerScript = GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy�ƐڐG���A���g���폜
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }

        // Score���Z --------------------------
        // �����_��
        if (collision.CompareTag("RandomBullet"))
        {
            Destroy(this.gameObject);

            // �X�R�A���Z
            scoreManagerScript.AddScore(RANDOM_BULLET_SCORE);
        }
        //// Player�Ɍ������Ă���
        //else if (collision.CompareTag("AimedBullet"))
        //{
        //    Destroy(this.gameObject);

        //    // �X�R�A���Z
        //    scoreManagerScript.AddScore(AIMED_BULLET_SCORE);
        //}
        //// �ǐ�
        //else if (collision.CompareTag("ChaseBullet"))
        //{
        //    Destroy(this.gameObject);

        //    scoreManagerScript.AddScore(CHASE_BULLET_SCORE);
        //}
    }
}
