using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollider : BulletBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player�ƐڐG���A���g���폜
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Debug.Log("damage");
        }
    }
}
