using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedEnemyBullet : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;

    void Start()
    {
        player = GameObject.Find("Player");

        enemy = GameObject.Find("Enemy");

        Vector2 vec = player.transform.position - enemy.transform.position;
        GetComponent<Rigidbody2D>().velocity = vec;

    }

    void Update()
    {
        move();
    }

    private void move()
    {

    }
}
