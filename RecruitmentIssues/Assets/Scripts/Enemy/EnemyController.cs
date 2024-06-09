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
    // �c��̗�
    [SerializeField] private int hitPoint = 100;

    private float moveTimer = 0.0f;

    // �ړ�����܂ł̃^�C��
    const float REVERSE_MOVE_TIME = 3.0f;
    // �����_���Ȓe�̔��ˊԊu�萔
    const float RANDOM_FIRE_INTERVAL = 0.25f;
    // Player�Ɍ������e�̔��ˊԊu�萔
    const float AIMED_FIRE_INTERVAL = 2.0f;
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
        // Player�Ɍ������e�̐������J�n
        StartCoroutine(SpawnAimedBullet());
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
            yield return new WaitForSeconds(AIMED_FIRE_INTERVAL);
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
