using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private float timer = 0.0f;

    // ���ݎ���     ����𒴂����delete
    [SerializeField] private float lifeTime = 0.0f;

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
        if (timer >= lifeTime)
        {
            return true;
        }

        return false;
    }

}
