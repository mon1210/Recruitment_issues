using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class Controller : MonoBehaviour
{
    // 弾Prefab取得
    [SerializeField] private GameObject bulletPrefab;
    // 爆弾Prefab取得
    [SerializeField] private GameObject bombPrefab;
    // 爆発エフェクトPrefab取得
    [SerializeField] private GameObject explosionPrefab;
    // 移動スピード
    [SerializeField] private int moveSpeed = 0;

    // キー入力を受け取って保存する用
    private Vector2 input = Vector2.zero;
    // スクリプト取得　IsMoveAble用
    private Collider colliderScript;
    // 減速フラグ
    private bool isLow = false;
    // 残り機数
    private int life = 3;

    // 弾発射位置調整用定数
    const float BULLET_OFFSET_Y = 1.0f;
    // 移動速度原則倍率定数
    const float SLOWDOWN_FACTOR = 0.5f;

    public Vector2 Input { get => input;}
    public int Life { get => life;}

    void Start()
    {
        colliderScript = GetComponent<Collider>();
    }

    void Update()
    {
        // 被ダメージ
        if(colliderScript.IsDamage)
        {
            life--;
            colliderScript.IsDamage = false;
        }

        // 残機確認
        checkLife();
    }

    // 移動関数
    private void move()
    {
        if(input.x != 0 || input.y != 0)
        {
            if (isLow)
            {
                // 減速時
                transform.Translate(input * moveSpeed * SLOWDOWN_FACTOR * Time.deltaTime);
            }
            else
            {
                // 入力された方向に移動
                transform.Translate(input * moveSpeed * Time.deltaTime);
            }
        }
    }

    // 攻撃関数
    private void fire()
    {
        // 位置調整
        bulletPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_Y, transform.position.y, transform.position.z);
        
        // 生成
        Instantiate(bulletPrefab);
    }

    // 爆撃関数
    private void bomb()
    {
        // 位置調整
        bombPrefab.transform.position = new Vector3(transform.position.x + BULLET_OFFSET_Y, transform.position.y, transform.position.z);

        // 生成
        Instantiate(bombPrefab);
    }

    // 残機確認
    private void checkLife()
    {
        if (life > 0)
        {
            // 移動可能時のみ移動
            if (colliderScript.IsMoveAble)
            {
                move();
            }
        }
        else
        {
            // 自身を非表示
            this.gameObject.SetActive(false);

            // 爆発アニメーション終了フラグを受け取ったら次に進む
            explosionEffect();

        }
    }

    // エフェクト生成
    private void explosionEffect()
    {
        explosionPrefab.transform.position = transform.position;

        Instantiate(explosionPrefab);
    }

    // 以下キー入力判定関数　================================================

    // 移動
    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();

        // Z軸で-90度回転しているので、正しく移動するように値を変換
        input = new Vector2(-rawInput.y, rawInput.x);
    }

    // 攻撃
    public void OnFireEvent(InputAction.CallbackContext context)
    {        
        // 左クリック or pad右トリガー を押したら
        if (context.phase == InputActionPhase.Performed)
        {
            fire();
        }
    }

    // 低速
    public void OnLowEvent(InputAction.CallbackContext context)
    {
        // 左クリック or pad右トリガー を押したら
        if (context.phase == InputActionPhase.Performed)
        {
            isLow = true;
        }
        // キーを離したら
        else if (context.phase == InputActionPhase.Canceled)
        {
            isLow = false;
        }
    }

    // 爆撃
    public void OnBombEvent(InputAction.CallbackContext context)
    {
        // 左クリック or pad右トリガー を押したら
        if (context.phase == InputActionPhase.Performed)
        {
            bomb();
        }
    }
}
