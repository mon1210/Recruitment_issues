using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    // �ړ����x
    [SerializeField] protected float moveSpeed = 0.0f;
    // �_�Ŏ���
    [SerializeField] protected float blinkInterval = 0.0f;

    // �_�Ńt���O
    protected bool isBlink = false;
    protected float timer = 0.0f;

    private float blinkTimer = 0.0f;

    // �_�Ŏ���
    const float BLINK_TIME = 1.0f;

    virtual protected void Start()
    {
        blinkTimer = BLINK_TIME;
    }

    virtual protected void Update()
    {
        // �_�ŏ���
        manageBlinking();
    }

    virtual protected void blinking() { }

    // �_�ł��w�肵���b�ԑ����悤�ɂ���
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
                // ���Z�b�g
                blinkTimer = BLINK_TIME;
                isBlink = false;
            }
        }
    }
}