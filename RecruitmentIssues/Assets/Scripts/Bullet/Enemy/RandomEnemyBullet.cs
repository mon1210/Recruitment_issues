using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyBullet : BulletBase
{
    // ����
    private Vector3 direction = Vector3.zero;
    // �ړ����x
    [SerializeField] private float moveSpeed = 0.0f;

    void Start()
    {     
        // �����_���Ȋp�x���擾
        float angle = Random.Range(0f, 360f);
        // �ʓx�@�֕ϊ�
        float radians = angle * Mathf.Deg2Rad;

        // �����x�N�g����ݒ�
        direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);

    }

    void Update()
    {
        // �����_���ȕ����Ɉړ�
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
