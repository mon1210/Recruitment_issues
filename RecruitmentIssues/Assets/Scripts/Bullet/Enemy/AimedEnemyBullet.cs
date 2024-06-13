using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedEnemyBullet : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;

    void Start()
    {
        // 自身がPrefabなのでFindを使用
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");

        // 自身の生成位置(Enemy)からPlayerまでのベクトルを取り、向かう
        Vector2 vec = player.transform.position - enemy.transform.position;
        // 進む位置を向くように
        transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        // 移動
        GetComponent<Rigidbody2D>().velocity = vec;

    }
}
