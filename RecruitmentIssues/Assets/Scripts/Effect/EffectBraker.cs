using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBraker : MonoBehaviour
{
    // アニメーション再生時間
    [SerializeField] private float animationTime = 0.0f;

    void Start()
    {
        // アニメーション再生終了後、自身を削除
        Invoke("destroy", animationTime);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
