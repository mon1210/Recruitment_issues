using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private float timer = 0.0f;

    // ���ݎ��Ԓ萔     ����𒴂����delete
    const float LIFETIME = 1.5f;

    void Update()
    {
        timer += Time.deltaTime;

        if (isDestroy())
        {
            Destroy(gameObject);
        }
    }

    // �폜����֐�
    private bool isDestroy()
    {
        // ��ʊO�ɏo����
        if (timer >= LIFETIME)
        {
            return true;
        }

        return false;
    }

}
