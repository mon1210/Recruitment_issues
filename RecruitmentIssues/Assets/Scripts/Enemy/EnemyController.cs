using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private EnemyCollider enemyCollider;
    private EnemyBulletManager enemyBulletManager;
    // �ړ����x
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private GameObject randomBulletPrefab;
    // �c��̗�
    [SerializeField] private int hitPoint = 100;

    private float moveTimer = 0.0f;

    // �ړ�����܂ł̃^�C��
    const float REVERSE_MOVE_TIME = 3.0f;
    // ���ˊԊu�萔
    const float FIRE_INTERVAL = 0.25f;
    // �e���ˈʒu�����p�萔
    const float BULLET_OFFSET_X = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<EnemyCollider>();
        enemyBulletManager = GetComponent<EnemyBulletManager>();

        // �����_���Ȓe�̐������J�n
        StartCoroutine(SpawnRandomBullet());
    }

    // Update is called once per frame
    void Update()
    {
        // ��_���[�W
        if(enemyCollider.IsDamage)
        {
            hitPoint--;
            enemyCollider.IsDamage = false;
        }

        if(hitPoint > 0)
        {
            // �ړ��\���݈̂ړ�
            if (enemyCollider.IsMoveAble)
            {
                move();
            }
        }
        else
        {
            // �@��0��GameOver     Todo ���j�A�j���[�V�����I��Event�ŃV�[���J��
            SceneManager.LoadScene("GameOverScene");
        }

    }

    // �ړ��֐�
    private void move()
    {
        moveTimer += Time.deltaTime;

        // �ŏ��͏�ړ�
        if (moveTimer >= REVERSE_MOVE_TIME)
        {
            // �ړ��������]
            moveSpeed *= -1.0f;
            moveTimer = 0.0f;
        }

        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    // �����_���Ȓe�𐶐�����
    private IEnumerator SpawnRandomBullet()
    {
        while (true) 
        { 
            Vector3 Pos = new Vector3(transform.position.x - BULLET_OFFSET_X, transform.position.y, transform.position.z);
            
            // ����
            GameObject RandomBullet = Instantiate(randomBulletPrefab, Pos, Quaternion.identity);
            
            // ���X�g�ɒǉ�
            enemyBulletManager.AddBulletList("Random", RandomBullet);

            // �C���^�[�o����҂�
            yield return new WaitForSeconds(FIRE_INTERVAL);
        }
    }
}
