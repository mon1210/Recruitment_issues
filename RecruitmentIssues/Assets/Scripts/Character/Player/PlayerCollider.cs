using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // スクリプト取得　Input用
    private PlayerController playerController;

    // 移動可能かどうかを表すフラグ　変更はここでする
    private bool isMoveAble = true;
    // 被ダメージフラグ
    private bool isDamage = false;

    // 画面の左下の座標
    Vector2 screenLeftBottom = Vector2.zero;
    // 画面の右上の座標
    Vector2 screenRightTop = Vector2.zero;

    // 中心点から画像の半分の大きさ
    const float OFFSET = 0.9f;

    public bool IsMoveAble { get => isMoveAble;}
    public bool IsDamage { get => isDamage; set => isDamage = value; }
    public Vector2 S_LeftBottom { get => screenLeftBottom; }
    public Vector2 S_RightTop { get => screenRightTop; }

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        // 座標を取得
        screenLeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

        screenRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void Update()
    {
        // 移動可能フラグ切り替え
        if(!isInView())
        {
            isMoveAble = false;
        }
        else if(isInView() && !isMoveAble)
        {
            isMoveAble = true;
        }
    }

    // 画面内にいるかを判断
    private bool isInView()
    {
        // 画面と座標を比較し、画面外に出ようとしているときに移動できないように
        if ((transform.position.x - OFFSET < screenLeftBottom.x && (playerController.Input.y < 0.0f)) ||   // 左端
            (transform.position.x + OFFSET > screenRightTop.x   && (playerController.Input.y > 0.0f)) ||   // 右端
            (transform.position.y + OFFSET > screenRightTop.y   && (playerController.Input.x < 0.0f)) ||   // 天井
            (transform.position.y - OFFSET < screenLeftBottom.y && (playerController.Input.x > 0.0f))      // 床
            ) 
        {
            return false; 
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemyの弾と接触時、被ダメージフラグをON
        if (collision.CompareTag("RandomBullet") || collision.CompareTag("AimedBullet") || collision.CompareTag("ChaseBullet"))
        {
            isDamage = true;
        }
    }

}
