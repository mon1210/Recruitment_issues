using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 三角関数を使用して角度を求める
/// </summary>
public class ChaseEnemyBullet : MonoBehaviour
{
    // 移動速度
    [SerializeField] private float moveSpeed = 0.0f;
    // 追跡する最大時間     最小0.1fにしないとエラーを吐く
    [SerializeField, Min(0.1f)] private float chaseLimitTime = 0.1f;

    private GameObject player;
    // 隣辺
    private float adjacent = 0.0f;
    // 対辺
    private float opposite = 0.0f;
    // 斜辺
    private float hypotenuse = 0.0f;
    // 追跡タイマー
    private float chaseTimer = 0.0f;

    void Start()
    {
        // 自身がPrefabなのでFindを使用
        player = GameObject.Find("Player");
    }

    void Update()
    {
        chaseTimer += Time.deltaTime;
        // 追跡時間内
        if(chaseTimer < chaseLimitTime)
        {
            // それぞれの辺を計算
            calculateTriangleSides();
        }

        // 移動
        move();
    }

    // 辺計算
    private void calculateTriangleSides()
    {
        // 隣辺
        adjacent = player.transform.position.x - this.transform.position.x;
        // 対辺
        opposite = player.transform.position.y - this.transform.position.y;

        // 斜辺 = √(隣辺^2 + 対辺^2)
        hypotenuse = Mathf.Sqrt(adjacent * adjacent + opposite * opposite);
    }

    private void move()
    {
        // 方向ベクトル(各辺)を正規化し、移動速度をかける
        transform.Translate(adjacent / hypotenuse * moveSpeed * Time.deltaTime, 
                            opposite / hypotenuse * moveSpeed * Time.deltaTime, 
                            0.0f
                            );
    }
}
