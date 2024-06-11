using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollider : MonoBehaviour
{
    // ��_���[�W�t���O
    private bool isDamage = false;

    public bool IsDamage { get => isDamage; set => isDamage = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // PlayerBullet�ƐڐG���_���[�W�t���OON
        if (collision.CompareTag("PlayerBullet"))
        {
            isDamage = true;
        }
    }
}
