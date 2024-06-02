using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private float timer = 0.0f;

    // ‘¶İŠÔ     ‚±‚ê‚ğ’´‚¦‚é‚Ædelete
    [SerializeField] private float lifeTime = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (isDestroy())
        {
            Destroy(gameObject);
        }
    }

    // íœ”»’èŠÖ”
    private bool isDestroy()
    {
        // ‰æ–ÊŠO‚Éo‚½
        if (timer >= lifeTime)
        {
            return true;
        }

        return false;
    }

}
