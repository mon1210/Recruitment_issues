using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollider : MonoBehaviour
{
    private ScoreManager scoreManager;

    // �G�̓_��
    const int ENEMY_SCORE = 5;
    // �����_���ɂ΂�܂����e�̓_��
    const int RANDOM_BULLET_SCORE = 3;
    // Player�Ɍ������Ĕ�Ԓe�̓_��
    const int AIMED_BULLET_SCORE = 5; 
    // �z�[�~���O�e�̓_��
    const int CHASE_BULLET_SCORE = 10;

    void Start()
    {
        // PlayerBullet��Prefab�̂��߁AFind���g�p
        scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }

    // �ڐG���Ɏ��g���폜���āA�X�R�A�����Z����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);

            // �X�R�A���Z
            scoreManager.AddScore(ENEMY_SCORE);
        }

        // �G�̒e -------------------------------------
        // �����_��
        if (collision.CompareTag("RandomBullet"))
        {
            Destroy(this.gameObject);

            // �X�R�A���Z
            scoreManager.AddScore(RANDOM_BULLET_SCORE);
        }
        // Player�Ɍ������Ĕ��
        else if (collision.CompareTag("AimedBullet"))
        {
            Destroy(this.gameObject);

            // �X�R�A���Z
            scoreManager.AddScore(AIMED_BULLET_SCORE);
        }
        // �z�[�~���O
        else if (collision.CompareTag("ChaseBullet"))
        {
            Destroy(this.gameObject);

            // �X�R�A���Z
            scoreManager.AddScore(CHASE_BULLET_SCORE);
        }
    }
}
