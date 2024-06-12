using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject dragon;
    [SerializeField] private GameObject dragonBottom;
    private EnemyController enemyControllerScript;
    private EnemyBulletManager enemyBulletManager;

    private bool isAimedStart = false;
    private bool isChaseStart = false;

    private float endTimer = 0.0f;

    const float END_TIME = 5.0f;

    public bool IsAimedStart { get => isAimedStart; set => isAimedStart = value; }
    public bool IsChaseStart { get => isChaseStart; set => isChaseStart = value; }

    void Start()
    {
        endTimer = 0.0f;

        enemyControllerScript = enemy.GetComponent<EnemyController>();
        enemyBulletManager = enemy.GetComponent<EnemyBulletManager>();
    }

    void Update()
    {
        // Player���S��
        if(!player.activeInHierarchy)
        {
            // Enemy�̃R�[���`��(�e����)�I��
            enemyControllerScript.StopAllCoroutines();

            // �Q�[���I��
            GameEnd();
        }

        // Enemy���S��
        if (!enemy.activeInHierarchy)
        {
            // �Q�[���I��
            GameEnd();
        }


        // �h���S����̎��S�A�z�[�~���O�e�J�n
        if (!dragon.activeInHierarchy && !dragonBottom.activeInHierarchy)
        {
            isChaseStart = true;
        }
        // �h���S����̎��S�APlayer�Ɍ������e�J�n�ielse if �ɂ��邱�Ƃœ�̖ڎ��S��ɂ�����x�ʂ邱�Ƃ�����j
        else if (!dragon.activeInHierarchy || !dragonBottom.activeInHierarchy)
        {
            isAimedStart = true;
        }

    }

    private void GameEnd()
    {
        // �G�̒e�폜
        enemyBulletManager.DestroyAllBullets("Random");
        enemyBulletManager.DestroyAllBullets("Aimed");
        enemyBulletManager.DestroyAllBullets("Chase");

        // 5�b���GameOverScene�֑J��
        endTimer += Time.deltaTime;
        if (endTimer >= END_TIME)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
