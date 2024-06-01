using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Controller : MonoBehaviour
{
    // �ePrefab�擾
    [SerializeField] private GameObject bulletPrefab;
    // �ړ��X�s�[�h
    [SerializeField] private int moveSpeed = 0;

    // �L�[���͂��󂯎���ĕۑ�����p
    private Vector2 input = Vector2.zero;
    // �X�N���v�g�擾�@IsMoveAble�p
    private Collider colliderScript;
    // �����t���O
    private bool isLow = false;

    // �e���ˈʒu�����p�萔
    const float BULLET_OFFSET_Y = 1.0f;
    // �ړ����x�����{���萔
    const float SLOWDOWN_FACTOR = 0.5f;

    public Vector2 Input { get => input;}

    void Start()
    {
        colliderScript = GetComponent<Collider>();
    }

    void Update()
    {
        // �ړ��\���݈̂ړ�
        if (colliderScript.IsMoveAble)
        {
            move();
        }
    }

    // �ړ��֐�
    private void move()
    {
        if(input.x != 0 || input.y != 0)
        {
            if (isLow)
            {
                // ������
                transform.Translate(input * moveSpeed * SLOWDOWN_FACTOR * Time.deltaTime);
            }
            else
            {
                // ���͂��ꂽ�����Ɉړ�
                transform.Translate(input * moveSpeed * Time.deltaTime);
            }
        }
    }

    // �U���֐�
    private void fire()
    {
        // �e�̈ʒu����
        bulletPrefab.transform.position = new Vector3(transform.position.x,transform.position.y + BULLET_OFFSET_Y, transform.position.z);
        
        // �e����
        Instantiate(bulletPrefab);
    }

    // �ړ��L�[�̓��͂��󂯎��
    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // �U���L�[�̓��͂��󂯎��
    public void OnFireEvent(InputAction.CallbackContext context)
    {        
        // ���N���b�N or pad�E�g���K�[ ����������
        if (context.phase == InputActionPhase.Performed)
        {
            fire();
        }
    }

    // �ᑬ�L�[�̓��͂��󂯎��
    public void OnLowEvent(InputAction.CallbackContext context)
    {
        // ���N���b�N or pad�E�g���K�[ ����������
        if (context.phase == InputActionPhase.Performed)
        {
            isLow = true;
        }
        // �L�[�𗣂�����
        else if (context.phase == InputActionPhase.Canceled)
        {
            isLow = false;
        }
    }
}
