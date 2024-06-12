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

    // 追跡タイマー
    private float chaseTimer = 0.0f;

    private Vector2 moveVec = Vector2.zero;

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
        moveVec.x = player.transform.position.x - this.transform.position.x;
        // 対辺
        moveVec.y = player.transform.position.y - this.transform.position.y;

        // 斜辺 = √(隣辺^2 + 対辺^2)
        float Hypotenuse = Mathf.Sqrt(moveVec.x * moveVec.x + moveVec.y * moveVec.y);

        // 方向ベクトル(各辺)を正規化し、移動速度をかける
        moveVec = new Vector2(moveVec.x / Hypotenuse * moveSpeed * Time.deltaTime,
                              moveVec.y / Hypotenuse * moveSpeed * Time.deltaTime);

    }

    private void move()
    {
        transform.Translate(moveVec);
    }
}
