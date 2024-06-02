using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : BulletBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemyと接触時、自身を削除
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
