using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : BulletBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy�ƐڐG���A���g���폜
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
