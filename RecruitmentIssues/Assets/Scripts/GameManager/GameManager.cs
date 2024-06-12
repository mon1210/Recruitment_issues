using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // PlayerŽæ“¾
    [SerializeField] private GameObject player;
    // EnemyŽæ“¾
    [SerializeField] private GameObject enemy;
    // ƒhƒ‰ƒSƒ““ª(ã)Žæ“¾
    [SerializeField] private GameObject dragon;
    // ƒhƒ‰ƒSƒ““ª(‰º)Žæ“¾
    [SerializeField] private GameObject dragonBottom;

    private EnemyController enemyControllerScript;
    private EnemyBulletManager enemyBulletManager;

    // I—¹ƒ^ƒCƒ}[
    private float endTimer = 0.0f;

    // Player‚ÉŒü‚©‚Á‚Ä”ò‚Ô’e¶¬ŠJŽnƒtƒ‰ƒO
    private bool isAimedStart = false;
    // ƒz[ƒ~ƒ“ƒO’e¶¬ŠJŽnƒtƒ‰ƒO
    private bool isChaseStart = false;

    // Player‚©Enemy‚ªŽ€–S‚µ‚Ä‚©‚çI—¹‚·‚é‚Ü‚Å‚ÌŽžŠÔ(Ž€–SŽž‚Ì”š”­ƒAƒjƒ[ƒVƒ‡ƒ“‚È‚Ç‚ðl—¶)
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
