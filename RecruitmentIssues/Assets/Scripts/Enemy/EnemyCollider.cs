using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // �X�N���[���T�C�Y�擾�p
    [SerializeField] private GameObject player;
    private Collider playerCollider;

    private EnemyController enemyController;
    // �ړ������𔽓]����t���O
    private bool isMoveAble = true;

    // ���S�_����摜�̔����̑傫��
    const float OFFSET = 1.8f;

    public bool IsMoveAble { get => isMoveAble; }

    void Start()
    {
        playerCollider = player.GetComponent<Collider>();
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        // �ړ��\�t���O�؂�ւ�
        if (!isInView())
        {
            isMoveAble = false;
        }
        else if (isInView() && !isMoveAble)
        {
            isMoveAble = true;
        }
    }

    // ��ʓ��ɂ��邩�𔻒f
    private bool isInView()
    {
        // ��ʂƍ��W���r���A��ʊO�ɏo�悤�Ƃ��Ă���Ƃ��Ɉړ��ł��Ȃ��悤��
        if ((transform.position.y + OFFSET > playerCollider.S_RightTop.y)   ||   // �V��
            (transform.position.y - OFFSET < playerCollider.S_LeftBottom.y)      // ��
            ) { return false; }


        return true;
    }
}
