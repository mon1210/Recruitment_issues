using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectBraker : MonoBehaviour
{
    // �A�j���[�V�����Đ�����
    const float ANIMATION_TIME = 1.1f;

    void Start()
    {
        // �A�j���[�V�����Đ��I����A���g���폜
        Invoke("destroy", ANIMATION_TIME);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
