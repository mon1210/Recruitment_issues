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

    private int deathCounter = 0;
    private float endTimer = 0.0f;

    const float END_TIME = 5.0f;

    public int DeathCounter { get => deathCounter; }

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

        // �h���S���j�󎞃J�E���g�𑝂₷�@��ԕω��p
        if(dragon.activeInHierarchy || dragonBottom.activeInHierarchy)
        {
            deathCounter++;
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
