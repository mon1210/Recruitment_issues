using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 時間経過でオブジェクトを削除する
/// </summary>
public class TimedDestruction : MonoBehaviour
{
    // アニメーション再生時間
    [SerializeField] private float lifeTime = 0.0f;

    void Start()
    {
        // 一定時間後、自身を削除
        Destroy(gameObject, lifeTime);
    }

}
