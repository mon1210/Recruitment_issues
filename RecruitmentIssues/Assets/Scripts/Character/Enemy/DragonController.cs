using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : CharacterBase
{
    private DragonCollider dragonCollider;
    private SpriteRenderer spriteRenderer;

    // 残り体力
    [SerializeField] private int hitPoint = 30;

    override protected void Start()
    {
        // 基底クラスのStart呼び出し
        base.Start();

        dragonCollider = GetComponent<DragonCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    override protected void Update()
    {
        // 基底クラスのUpdate呼び出し
        base.Update();

        // 点滅以外で赤になっていた場合、色修正
        if (!isBlink && spriteRenderer.color == Color.red)
        {
            spriteRenderer.color = Color.white;
        }

        // 被ダメージ
        if(dragonCollider.IsDamage)
        {
            hitPoint--;
            dragonCollider.IsDamage = false;
            isBlink = true;
        }

        // 体力ゼロ
        if (hitPoint < 0)
        {
            // 自身を非表示
            this.gameObject.SetActive(false);

            // 爆発エフェクト表示

                
        }
    }

    // 点滅処理
    override protected void blinking()
    {
        // 内部時刻を経過させる
        timer += Time.deltaTime;

        // timer～blinkIntervalの間で繰り返し値を取得
        float repeatValue = Mathf.Repeat((float)timer, blinkInterval);

        // インターバルの半分以上の時は赤色
        if (repeatValue >= blinkInterval * 0.5f)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }
}
