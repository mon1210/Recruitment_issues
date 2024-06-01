using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Controller : MonoBehaviour
{
    // 弾Prefab取得
    [SerializeField] private GameObject bulletPrefab;
    // 移動スピード
    [SerializeField] private int moveSpeed = 0;

    // キー入力を受け取って保存する用
    private Vector2 input = Vector2.zero;
    // スクリプト取得　IsMoveAble用
    private Collider colliderScript;

    // 弾発射位置調整用定数
    const float BULLET_OFFSET_Y = 1.0f;

    public Vector2 Input { get => input;}

    // Start is called before the first frame update
    void Start()
    {
        colliderScript = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動可能時のみ移動
        if (colliderScript.IsMoveAble)
        {
            move();
        }
    }

    // 移動関数
    private void move()
    {
        if(input.x != 0 || input.y != 0)
        {
            // 入力された方向に移動
            transform.Translate(input * moveSpeed * Time.deltaTime);
        }
    }

    // 攻撃関数
    private void fire()
    {
        // 弾の位置調整
        bulletPrefab.transform.position = new Vector3(transform.position.x,transform.position.y + BULLET_OFFSET_Y, transform.position.z);
        
        // 弾生成
        Instantiate(bulletPrefab);
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
            fire();
        }
    }
}
