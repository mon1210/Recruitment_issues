using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private EnemyCollider enemyCollider;
    private EnemyBulletManager enemyBulletManager;
    private SpriteRenderer spriteRenderer;

    // �ړ����x
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private GameObject randomBulletPrefab;
    [SerializeField] private GameObject aimedBulletPrefab;
    // �c��̗�
    [SerializeField] private int hitPoint = 100;
    // �_�Ŏ���
    [SerializeField] private float blinkInterval = 0.0f;

    private float moveTimer = 0.0f;
    private float blinkTimer = 0.0f;
    private float timer = 0.0f;
    private bool isBlink = false;

    // �ړ�����܂ł̃^�C��
    const float REVERSE_MOVE_TIME = 3.0f;
    // ���ˊԊu�萔
    const float FIRE_INTERVAL = 0.25f;
    // 
    const float AIMED_FIRE_INTERVAL = 2.0f;
    // �e���ˈʒu�����p�萔
    const float BULLET_OFFSET_X = 1.0f;
    // �_�Ŏ���
    const float BLINK_TIME = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<EnemyCollider>();
        enemyBulletManager = GetComponent<EnemyBulletManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �����_���Ȓe�̐������J�n
        StartCoroutine(SpawnRandomBullet());
        // 
        StartCoroutine(SpawnAimedBullet());
    }

    // Update is called once per frame
    void Update()
    {
        // ��_���[�W
        if(enemyCollider.IsDamage)
        {
            hitPoint--;
            isBlink = true;
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

        manageBlinking();
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

    // �_�ŏ���
    private void blinking()
    {
        // �����������o�߂�����
        timer += Time.deltaTime;

        // timer�`blinkInterval�̊ԂŌJ��Ԃ��l���擾
        float repeatValue = Mathf.Repeat((float)timer, blinkInterval);

        // �C���^�[�o���̔����ȏ�̎��͕\��
        if (repeatValue >= blinkInterval * 0.5f)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }

    // �_�ł��w�肵���b�ԑ����悤�ɂ���
    private void manageBlinking()
    {
        if (isBlink)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer > 0)
            {
                blinking();
            }
            else
            {
                // ���Z�b�g
                blinkTimer = BLINK_TIME;
                isBlink = false;
            }
        }
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

    // Player�Ɍ������Ĕ�Ԓe�𐶐�����
    private IEnumerator SpawnAimedBullet()
    {
        while (true)
        {
            Vector3 Pos = new Vector3(transform.position.x - BULLET_OFFSET_X, transform.position.y, transform.position.z);

            // ����
            GameObject AimedBullet = Instantiate(aimedBulletPrefab, Pos, Quaternion.identity);

            // ���X�g�ɒǉ�
            enemyBulletManager.AddBulletList("Aimed", AimedBullet);

            // �C���^�[�o����҂�
            yield return new WaitForSeconds(AIMED_FIRE_INTERVAL);
        }
    }
}
