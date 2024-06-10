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
        GetComponent<Rigidbody2D>().velocity = vec;

    }
}
