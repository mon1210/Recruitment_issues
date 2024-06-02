using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollider : BulletBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player‚ÆÚGA©g‚ğíœ
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Debug.Log("damage");
        }
    }
}
