using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Controller : MonoBehaviour
{
    // �L�[���͂��󂯎���ĕۑ�����p
    Vector2 input = Vector2.zero;

    // 
    [SerializeField] private int MOVE_SPEED = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    // �ړ��֐�
    private void move()
    {
        if(input.x != 0 || input.y != 0)
        {
            // ���͂��ꂽ�����Ɉړ�
            transform.Translate(input * MOVE_SPEED * Time.deltaTime);
        }
    }

    // �L�[���͂̒l���󂯎��
    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}