using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : CharacterBase
{
    // �����G�t�F�N�gPrefab�擾
    [SerializeField] private GameObject explosionPrefab;
    // �X�R�A�Ǘ��X�N���v�g�擾
    [SerializeField] private ScoreManager scoreManager;
    // �c��̗�
    [SerializeField] private int hitPoint = 30;

    private SpriteRenderer spriteRenderer;
    private DragonCollider dragonCollider;

    // ���j���̉��Z�X�R�A
    const int SCORE = 500;

    override protected void Start()
    {
        // ���N���X��Start�Ăяo��
        base.Start();

        dragonCollider = GetComponent<DragonCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        if(dragonCollider.IsDamage)
        {
            hitPoint--;
            dragonCollider.IsDamage = false;
            isBlink = true;
        }

        // �̗̓[��
        if (hitPoint <= 0)
        {
            // �X�R�A���Z
            scoreManager.AddScore(SCORE);

            // ���g���\��
            this.gameObject.SetActive(false);

            // �����G�t�F�N�g�\��
            explosionEffect();

        }
    }

    // �G�t�F�N�g����
    private void explosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
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
}
