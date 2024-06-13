using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : CharacterBase
{
    // 爆発エフェクトPrefab取得
    [SerializeField] private GameObject explosionPrefab;
    // スコア管理スクリプト取得
    [SerializeField] private ScoreManager scoreManager;
    // 残り体力
    [SerializeField] private int hitPoint = 30;

    private SpriteRenderer spriteRenderer;
    private DragonCollider dragonCollider;

    // 撃破時の加算スコア
    const int SCORE = 500;

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
        if (hitPoint <= 0)
        {
            // スコア加算
            scoreManager.AddScore(SCORE);

            // 自身を非表示
            this.gameObject.SetActive(false);

            // 爆発エフェクト表示
            explosionEffect();

        }
    }

    // エフェクト生成
    private void explosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
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
