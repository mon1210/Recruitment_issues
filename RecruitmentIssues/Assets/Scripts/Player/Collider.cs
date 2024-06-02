using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    // ��ʂ̍����̍��W
    Vector2 screenLeftBottom = Vector2.zero;
    // ��ʂ̉E��̍��W
    Vector2 screenRightTop = Vector2.zero;

    // �ړ��\���ǂ�����\���t���O�@�ύX�͂����ł���
    private bool isMoveAble = true;
    // �X�N���v�g�擾�@Input�p
    private Controller controllerScripts;

    // ���S�_����摜�̔����̑傫��
    const float OFFSET = 0.9f;

    public bool IsMoveAble { get => isMoveAble;}

    void Start()
    {
        controllerScripts = GetComponent<Controller>();

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
        if ((transform.position.x - OFFSET < screenLeftBottom.x && (controllerScripts.Input.x < 0.0f)) ||   // ���[
            (transform.position.x + OFFSET > screenRightTop.x   && (controllerScripts.Input.x > 0.0f)) ||   // �E�[
            (transform.position.y + OFFSET > screenRightTop.y   && (controllerScripts.Input.y > 0.0f)) ||   // �V��
            (transform.position.y - OFFSET < screenLeftBottom.y && (controllerScripts.Input.y < 0.0f))      // ��
            ) { return false; }


        return true;
    }

}