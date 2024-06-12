using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    // 存在時間     これを超えるとdelete
    [SerializeField] private float lifeTime = 0.0f;

    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (isDestroy())
        {
            Destroy(gameObject);
        }
    }

    // 削除判定関数
    private bool isDestroy()
    {
        // 画面外に出た時
        if (timer >= lifeTime)
        {
            return true;
        }

        return false;
    }

}
