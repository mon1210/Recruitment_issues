using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyBullet : BulletBase
{
    // 方向
    private Vector3 direction = Vector3.zero;
    // 移動速度
    [SerializeField] private float moveSpeed = 0.0f;

    void Start()
    {     
        // ランダムな角度を取得
        float angle = Random.Range(0f, 360f);
        // 弧度法へ変換
        float radians = angle * Mathf.Deg2Rad;

        // 方向ベクトルを設定
        direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);

    }

    void Update()
    {
        // ランダムな方向に移動
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
