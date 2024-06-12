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


        // ƒhƒ‰ƒSƒ““ñ‘ÌŽ€–SAƒz[ƒ~ƒ“ƒO’eŠJŽn
        if (!dragon.activeInHierarchy && !dragonBottom.activeInHierarchy)
        {
            isChaseStart = true;
        }
        // ƒhƒ‰ƒSƒ“ˆê‘ÌŽ€–SAPlayer‚ÉŒü‚©‚¤’eŠJŽnielse if ‚É‚·‚é‚±‚Æ‚Å“ñ‘Ì–ÚŽ€–SŒã‚É‚à‚¤ˆê“x’Ê‚é‚±‚Æ‚ð‰ñ”ðj
        else if (!dragon.activeInHierarchy || !dragonBottom.activeInHierarchy)
        {
            isAimedStart = true;
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
