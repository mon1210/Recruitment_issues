using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollider : MonoBehaviour
{
    // 被ダメージフラグ
    private bool isDamage = false;

    public bool IsDamage { get => isDamage; set => isDamage = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // PlayerBulletと接触時ダメージフラグON
        if (collision.CompareTag("PlayerBullet"))
        {
            isDamage = true;
        }
    }
}
