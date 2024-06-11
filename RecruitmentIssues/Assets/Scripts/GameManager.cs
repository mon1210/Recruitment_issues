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
        // PlayerŽ€–SŽž
        if(!player.activeInHierarchy)
        {
            // Enemy‚ÌƒR[ƒ‹ƒ`ƒ“(’e¶¬)I—¹
            enemyControllerScript.StopAllCoroutines();

            // ƒQ[ƒ€I—¹
            GameEnd();
        }

        // EnemyŽ€–SŽž
        if (!enemy.activeInHierarchy)
        {
            // ƒQ[ƒ€I—¹
            GameEnd();
        }
    }

    private void GameEnd()
    {
        // “G‚Ì’eíœ
        enemyBulletManager.DestroyAllBullets("Random");
        enemyBulletManager.DestroyAllBullets("Aimed");
        enemyBulletManager.DestroyAllBullets("Chase");

        // 5•bŒã‚ÉGameOverScene‚Ö‘JˆÚ
        endTimer += Time.deltaTime;
        if (endTimer >= END_TIME)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
