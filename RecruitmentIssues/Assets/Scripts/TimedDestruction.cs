using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ԍo�߂ŃI�u�W�F�N�g���폜����
/// </summary>
public class TimedDestruction : MonoBehaviour
{
    // �A�j���[�V�����Đ�����
    [SerializeField] private float lifeTime = 0.0f;

    void Start()
    {
        // ��莞�Ԍ�A���g���폜
        Destroy(gameObject, lifeTime);
    }

}
