using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBraker : MonoBehaviour
{
    // �A�j���[�V�����Đ�����
    [SerializeField] private float animationTime = 0.0f;

    void Start()
    {
        // �A�j���[�V�����Đ��I����A���g���폜
        Invoke("destroy", animationTime);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
