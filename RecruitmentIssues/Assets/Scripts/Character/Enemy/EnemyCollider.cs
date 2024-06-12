using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // スクリーンサイズ取得用
    [SerializeField] private GameObject player;

    private PlayerCollider playerCollider;

    // 移動方向を反転するフラグ
    private bool isMoveAble = true;
    // 被ダメージフラグ
    private bool isDamage = false;

    // 中心点から画像の半分の大きさ
    const float OFFSET = 1.8f;

    public bool IsMoveAble { get => isMoveAble; }
    public bool IsDamage { get => isDamage; set => isDamage = value; }

    void Start()
    {
        playerCollider = player.GetComponent<PlayerCollider>();
    }

    void Update()
    {
        // 移動可能フラグ切り替え
        if (!isInView())
        {
            isMoveAble = false;
        }
        else if (isInView() && !isMoveAble)
        {
            isMoveAble = true;
        }
    }

    // 画面内にいるかを判断
    private bool isInView()
    {
        // 画面と座標を比較し、画面外に出ようとしているときに移動できないように
        if ((transform.position.y + OFFSET > playerCollider.S_RightTop.y)   ||   // 天井
            (transform.position.y - OFFSET < playerCollider.S_LeftBottom.y)      // 床
            ) { return false; }


        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // PlayerBulletと接触時ダメージフラグON
        if (collision.CompareTag("PlayerBullet"))
        {
            isDamage = true;
        }
    }
}
