using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Controller : MonoBehaviour
{
    // キー入力を受け取って保存する用
    Vector2 input = Vector2.zero;

    // 移動スピード
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

    // 移動関数
    private void move()
    {
        if(input.x != 0 || input.y != 0)
        {
            // 入力された方向に移動
            transform.Translate(input * MOVE_SPEED * Time.deltaTime);
        }
    }

    // 移動キーの入力を受け取る
    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // 攻撃キーの入力を受け取る
    public void OnFireEvent(InputAction.CallbackContext context)
    {        
        // 左クリック or pad右トリガー を押したら
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Fire");
        }
    }
}
