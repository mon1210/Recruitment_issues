using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    // �ړ����x
    [SerializeField] private int moveSpeed = 0;

    void Update()
    {
        move();
    }

    private void move()
    {
        // Z����-90�x��]���Ă���̂ŁAY�������Ƀv���X�ŉE�ړ�
        transform.Translate(0.0f, moveSpeed * Time.deltaTime, 0.0f);
    }

}
