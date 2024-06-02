using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // 
    [SerializeField] Collider colliderScript;

    private EnemyController enemyController;
    // 移動可能かどうかを表すフラグ　変更はここでする
    private bool isMoveAble = true;

    // 中心点から画像の半分の大きさ
    const float OFFSET = 0.9f;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
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
        if ((transform.position.y + OFFSET > colliderScript.S_RightTop.y    /*&& (enemyController.Input.x < 0.0f)*/) ||   // 天井
            (transform.position.y - OFFSET < colliderScript.S_LeftBottom.y  /*&& (enemyController.Input.x > 0.0f)*/)      // 床
            ) { return false; }


        return true;
    }
}
