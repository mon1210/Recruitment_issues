using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bomb�ɂ̂݃X�N���v�g������ƁA�ʃI�u�W�F�N�g�ŎQ�Ƃł��Ȃ��̂ŊǗ��X�N���v�g��p��
/// Bomb���Ƃ����Ɏ��g�������Ă��܂��̂ŁA�G�̒e���폜������Ȃ�
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

    // ���ˈʒu�����p�萔
    const float OFFSET_Y = 1.0f;
    // �����܂ł̎���
    const float EXPLOSION_START = 1.5f;
    // �����I������
    const float EXPLOSION_END = 2.0f;
    // �����_�������̒e�𔚒e�ŉ󂵂��Ƃ��̉��Z�X�R�A
    const int RANDOM_BULLET_BREAK_SCORE = 10;
    // Player�Ɍ������Ĕ�Ԓe�𔚒e�ŉ󂵂��Ƃ��̉��Z�X�R�A
    const int AIMED_BULLET_BREAK_SCORE = 30;
    // �z�[�~���O�e�𔚒e�ŉ󂵂��Ƃ��̉��Z�X�R�A
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
            // �ʒu����(���̏ꍇ��transform.position��Player)
            Vector3 Pos = new Vector3(transform.position.x + OFFSET_Y, transform.position.y, transform.position.z);

            // ����
            Instantiate(bombPrefab, Pos, Quaternion.identity);

            controllerScript.IsBombInstantiate = false;

            isTimerStart = true;
        }

        // ����
        if (isTimerStart)
        {
            timer += Time.deltaTime;
            // 0.5�b�ԁA�G�̒e�폜
            if(timer >= EXPLOSION_START)
            {
                // �������e�ɉ����ăX�R�A���Z
                int DestroyedCount = enemyBulletManager.DestroyAllBullets("Random");
                scoreManager.AddScore(DestroyedCount * RANDOM_BULLET_BREAK_SCORE);

                DestroyedCount = enemyBulletManager.DestroyAllBullets("Aimed");
                scoreManager.AddScore(DestroyedCount * AIMED_BULLET_BREAK_SCORE);

                DestroyedCount = enemyBulletManager.DestroyAllBullets("Chase");
                scoreManager.AddScore(DestroyedCount * CHASE_BULLET_BREAK_SCORE);
            }
            // ���Z�b�g
            if(timer >= EXPLOSION_END)
            {
                timer = 0;
                isTimerStart = false;
            }
        }
    }
}
