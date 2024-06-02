using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : BulletBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy‚ÆÚGA©g‚ğíœ
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
