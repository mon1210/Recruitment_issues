using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // �X�N���v�g�擾�@Input�p
    private PlayerController playerController;

    // �ړ��\���ǂ�����\���t���O�@�ύX�͂����ł���
    private bool isMoveAble = true;
    // ��_���[�W�t���O
    private bool isDamage = false;

    // ��ʂ̍����̍��W
    Vector2 screenLeftBottom = Vector2.zero;
    // ��ʂ̉E��̍��W
    Vector2 screenRightTop = Vector2.zero;

    // ���S�_����摜�̔����̑傫��
    const float OFFSET = 0.9f;

    public bool IsMoveAble { get => isMoveAble;}
    public bool IsDamage { get => isDamage; set => isDamage = value; }
    public Vector2 S_LeftBottom { get => screenLeftBottom; }
    public Vector2 S_RightTop { get => screenRightTop; }

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        // ���W���擾
        screenLeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

        screenRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void Update()
    {
        // �ړ��\�t���O�؂�ւ�
        if(!isInView())
        {
            isMoveAble = false;
        }
        else if(isInView() && !isMoveAble)
        {
            isMoveAble = true;
        }
    }

    // ��ʓ��ɂ��邩�𔻒f
    private bool isInView()
    {
        // ��ʂƍ��W���r���A��ʊO�ɏo�悤�Ƃ��Ă���Ƃ��Ɉړ��ł��Ȃ��悤��
        if ((transform.position.x - OFFSET < screenLeftBottom.x && (playerController.Input.y < 0.0f)) ||   // ���[
            (transform.position.x + OFFSET > screenRightTop.x   && (playerController.Input.y > 0.0f)) ||   // �E�[
            (transform.position.y + OFFSET > screenRightTop.y   && (playerController.Input.x < 0.0f)) ||   // �V��
            (transform.position.y - OFFSET < screenLeftBottom.y && (playerController.Input.x > 0.0f))      // ��
            ) 
        {
            return false; 
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy�̒e�ƐڐG���A��_���[�W�t���O��ON
        if (collision.CompareTag("RandomBullet") || collision.CompareTag("AimedBullet") || collision.CompareTag("ChaseBullet"))
        {
            isDamage = true;
        }
    }

}
