using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyBullet : MonoBehaviour
{
    // �ړ����x
    [SerializeField] private float moveSpeed = 0.0f;

    // ����
    private Vector3 direction = Vector3.zero;

    void Start()
    {     
        // �����_���Ȋp�x���擾
        float Angle = Random.Range(0f, 360f);
        // �ʓx�@�֕ϊ�
        float Radians = Angle * Mathf.Deg2Rad;

        // �e���v���C���[���ɗ���悤�ɂ���
        float DirectionX = Mathf.Cos(Radians);
        if(DirectionX > 0)
        {
            DirectionX *= -1;
        }

        // �����x�N�g����ݒ�
        direction = new Vector3(DirectionX, Mathf.Sin(Radians), 0);

    }

    void Update()
    {
        // �����_���ȕ����Ɉړ�
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
