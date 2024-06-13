using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyBullet : MonoBehaviour
{
    // 移動速度
    [SerializeField] private float moveSpeed = 0.0f;

    // 方向
    private Vector3 direction = Vector3.zero;

    void Start()
    {     
        // ランダムな角度を取得
        float Angle = Random.Range(0f, 360f);
        // 弧度法へ変換
        float Radians = Angle * Mathf.Deg2Rad;

        // 弾がプレイヤー側に来るようにする
        float DirectionX = Mathf.Cos(Radians);
        if(DirectionX > 0)
        {
            DirectionX *= -1;
        }

        // 方向ベクトルを設定
        direction = new Vector3(DirectionX, Mathf.Sin(Radians), 0);

    }

    void Update()
    {
        // ランダムな方向に移動
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
