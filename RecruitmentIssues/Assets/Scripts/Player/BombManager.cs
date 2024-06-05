using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bombにのみスクリプトがあると、別オブジェクトで参照できないので管理スクリプトを用意
/// </summary>
public class BombManager : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    
    private Controller controllerScript;

    // 爆発したかどうかを判断
    private bool isExplosion = false;

    // 発射位置調整用定数
    const float OFFSET_Y = 1.0f;
    // 爆発までの時間
    const float EXPLOSION_TIMER = 1.5f;

    public bool IsExplosion { get => isExplosion; set => isExplosion = value; }

    void Start()
    {
        controllerScript = GetComponent<Controller>();

        // 発生後、一秒後に爆発
        Invoke("isDestroy", EXPLOSION_TIMER);
    }

    // 消えたかどうかを判断
    private void isDestroy()
    {
        // 別の箇所でfalseにする
        isExplosion = true;
    }

    void Update()
    {
        if(controllerScript.IsBombInstantiate)
        {
            // 位置調整(この場合のtransform.positionはPlayer)
            bombPrefab.transform.position = new Vector3(transform.position.x + OFFSET_Y, transform.position.y, transform.position.z);

            // 生成
            Instantiate(bombPrefab);

            controllerScript.IsBombInstantiate = false;
        }
    }

}
