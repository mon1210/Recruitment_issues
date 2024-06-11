using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : CharacterBase
{
    private EnemyCollider enemyCollider;
    private EnemyBulletManager enemyBulletManager;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject randomBulletPrefab;
    [SerializeField] private GameObject aimedBulletPrefab;
    [SerializeField] private GameObject chaseBulletPrefab;
    // �����G�t�F�N�gPrefab�擾
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private GameManager gameManager;
    // �c��̗�
    [SerializeField] private int hitPoint = 100;

    private float moveTimer = 0.0f;
    private bool isStartAimedBullet = false;
    private bool isStartChaseBullet = false;

    // �ړ�����܂ł̃^�C��
    const float REVERSE_MOVE_TIME = 3.0f;
    // �����_���Ȓe�̔��ˊԊu�萔
    const float RANDOM_FIRE_INTERVAL = 0.25f;
    // Player�Ɍ������e�̔��ˊԊu�萔
    const float AIMED_FIRE_INTERVAL = 2.0f;
    // �z�[�~���O�e�̔��ˊԊu�萔
    const float CHASE_FIRE_INTERVAL = 5.0f;
    // �e���ˈʒu�����p�萔
    const float BULLET_OFFSET_X = 1.0f;

    override protected void Start()
    {
        // ���N���X��Start�Ăяo��
        base.Start();

        enemyCollider = GetComponent<EnemyCollider>();
        enemyBulletManager = GetComponent<EnemyBulletManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �����_���Ȓe�̐������J�n
        StartCoroutine(SpawnRandomBullet());
    }

    override protected void Update()
    {
        // ���N���X��Update�Ăяo��
        base.Update();

        // �_�ňȊO�ŐԂɂȂ��Ă����ꍇ�A�F�C��
        if (!isBlink && spriteRenderer.color == Color.red)
        {
            spriteRenderer.color = Color.white;
        }

        // ��_���[�W
        if (enemyCollider.IsDamage)
        {
            hitPoint--;
            enemyCollider.IsDamage = false;
            isBlink = true;
        }

        if (hitPoint > 0)
        {
            // �ړ��\���݈̂ړ�
            if (enemyCollider.IsMoveAble)
            {
                move();
            }
        }
        else
        {
            // ���g���\��
            this.gameObject.SetActive(false);

            // �����G�t�F�N�g�\��
            explosionEffect();
        }

        // Player�Ɍ������Ĕ�Ԓe�̐����J�n
        if (gameManager.IsAimedStart && !isStartAimedBullet)
        {
            StartCoroutine(SpawnAimedBullet());
            gameManager.IsAimedStart = false;
            isStartAimedBullet = true;
        }
        // �z�[�~���O�e�̐����J�n
        if (gameManager.IsChaseStart && !isStartChaseBullet)
        {
            StartCoroutine(SpawnChaseBullet());
            gameManager.IsChaseStart = false;
            isStartChaseBullet = true;
        }
    }

    // �G�t�F�N�g����
    private void explosionEffect()
    {
        Vector3 defaultSize = explosionPrefab.transform.localScale;

        // �T�C�Y�ύX
        explosionPrefab.transform.localScale = new Vector3(7.0f, 7.0f, 7.0f);

        // ����
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // �h���S���ł��g���܂킷�̂Ō��ɖ߂�
        explosionPrefab.transform.localScale = defaultSize;
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
    override protected void blinking()
    {
        // �����������o�߂�����
        timer += Time.deltaTime;

        // timer�`blinkInterval�̊ԂŌJ��Ԃ��l���擾
        float repeatValue = Mathf.Repeat((float)timer, blinkInterval);

        // �C���^�[�o���̔����ȏ�̎��͐ԐF
        if (repeatValue >= blinkInterval * 0.5f)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.red;
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
            yield return new WaitForSeconds(RANDOM_FIRE_INTERVAL);
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

    // �z�[�~���O�e�𐶐�����
    private IEnumerator SpawnChaseBullet()
    {
        while (true)
        {
            Vector3 Pos = new Vector3(transform.position.x - BULLET_OFFSET_X, transform.position.y, transform.position.z);

            // ����
            GameObject ChaseBullet = Instantiate(chaseBulletPrefab, Pos, Quaternion.identity);

            // ���X�g�ɒǉ�
            enemyBulletManager.AddBulletList("Chase", ChaseBullet);

            // �C���^�[�o����҂�
            yield return new WaitForSeconds(CHASE_FIRE_INTERVAL);
        }
    }
}
