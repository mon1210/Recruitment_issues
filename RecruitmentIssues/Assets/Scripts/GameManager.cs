using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    private EnemyController enemyControllerScript;
    private EnemyBulletManager enemyBulletManager;

    private float endTimer = 0.0f;

    const float END_TIME = 5.0f;

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
