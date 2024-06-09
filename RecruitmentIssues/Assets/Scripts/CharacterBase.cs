using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    // 移動速度
    [SerializeField] protected float moveSpeed = 0.0f;
    // 点滅周期
    [SerializeField] protected float blinkInterval = 0.0f;

    // 点滅フラグ
    protected bool isBlink = false;
    protected float timer = 0.0f;

    private float blinkTimer = 0.0f;

    // 点滅時間
    const float BLINK_TIME = 1.0f;

    virtual protected void Start()
    {
        blinkTimer = BLINK_TIME;
    }

    virtual protected void Update()
    {
        // 点滅処理
        manageBlinking();
    }

    virtual protected void blinking() { }

    // 点滅が指定した秒間続くようにする
    protected void manageBlinking()
    {
        if (isBlink)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer > 0)
            {
                blinking();
            }
            else
            {
                // リセット
                blinkTimer = BLINK_TIME;
                isBlink = false;
            }
        }
    }
}
