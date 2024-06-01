using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    private float timer = 0.0f;

    // ‘¶ÝŽžŠÔ’è”     ‚±‚ê‚ð’´‚¦‚é‚Ædelete
    const float LIFETIME = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
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
        // ‰æ–ÊŠO‚Éo‚½Žž
        if (timer >= LIFETIME)
        {
            return true;
        }

        return false;
    }
}
