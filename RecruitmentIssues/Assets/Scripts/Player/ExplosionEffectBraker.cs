using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectBraker : MonoBehaviour
{
    // アニメーション再生時間
    const float ANIMATION_TIME = 1.1f;

    void Start()
    {
        // アニメーション再生終了後、自身を削除
        Invoke("destroy", ANIMATION_TIME);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
