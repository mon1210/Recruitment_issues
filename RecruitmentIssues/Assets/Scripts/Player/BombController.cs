using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    // 点滅周期
    [SerializeField] private float blinkInterval = 0.0f;

    private SpriteRenderer spriteRenderer;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        blinking();
    }

    // 点滅関数
    private void blinking()
    {
        // 内部時刻を経過させる
        timer += Time.deltaTime;

        // timer～blinkIntervalの間で繰り返し値を取得
        float repeatValue = Mathf.Repeat((float)timer, blinkInterval);

        // インターバルの半分以上の時は表示
        if(repeatValue >= blinkInterval * 0.5f)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }

    }
}
