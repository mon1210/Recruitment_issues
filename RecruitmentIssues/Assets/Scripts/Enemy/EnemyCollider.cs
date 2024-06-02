using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // 
    [SerializeField] Collider colliderScript;

    private EnemyController enemyController;
    // �ړ��\���ǂ�����\���t���O�@�ύX�͂����ł���
    private bool isMoveAble = true;

    // ���S�_����摜�̔����̑傫��
    const float OFFSET = 0.9f;

    void Start()
    {
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
        if ((transform.position.y + OFFSET > colliderScript.S_RightTop.y    /*&& (enemyController.Input.x < 0.0f)*/) ||   // �V��
            (transform.position.y - OFFSET < colliderScript.S_LeftBottom.y  /*&& (enemyController.Input.x > 0.0f)*/)      // ��
            ) { return false; }


        return true;
    }
}
