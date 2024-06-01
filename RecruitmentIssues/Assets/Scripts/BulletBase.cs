using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private float timer = 0.0f;

    // 存在時間定数     これを超えるとdelete
    const float LIFETIME = 1.5f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (isDestroy())
        {
            Destroy(gameObject);
        }
    }

    // 削除判定関数
    protected bool isDestroy()
    {
        // 画面外に出た時
        if (timer >= LIFETIME)
        {
            return true;
        }

        return false;
    }

}
