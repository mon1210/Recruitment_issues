using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedEnemyBullet : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;

    void Start()
    {
        // ���g��Prefab�Ȃ̂�Find���g�p
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");

        // ���g�̐����ʒu(Enemy)����Player�܂ł̃x�N�g�������A������
        Vector2 vec = player.transform.position - enemy.transform.position;
        // �i�ވʒu�������悤��
        transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
        // �ړ�
        GetComponent<Rigidbody2D>().velocity = vec;

    }
}
