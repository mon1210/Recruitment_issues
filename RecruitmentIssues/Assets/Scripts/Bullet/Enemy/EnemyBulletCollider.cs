using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Playerと接触時、自身を削除
        if(collision.CompareTag("Player") || collision.CompareTag("PlayerBullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
