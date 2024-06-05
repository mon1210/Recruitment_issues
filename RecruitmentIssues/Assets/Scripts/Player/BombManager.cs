using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bombにのみスクリプトがあると、別オブジェクトで参照できないので管理スクリプトを用意
/// Bombだとすぐに自身が消えてしまうので、敵の弾を削除しきれない
/// </summary>
public class BombManager : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    
    private Controller controllerScript;
    private float timer = 0;
    private bool isTimerStart = false;

    // 発射位置調整用定数
    const float OFFSET_Y = 1.0f;
    // 爆発までの時間
    const float EXPLOSION_START = 1.5f;
    // 爆発終了時間
    const float EXPLOSION_END = 2.0f;

    void Start()
    {
        controllerScript = GetComponent<Controller>();
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

            isTimerStart = true;
        }

        // 爆発
        if (isTimerStart)
        {
            timer += Time.deltaTime;
            // 0.5秒間、敵の弾削除
            if(timer >= EXPLOSION_START)
            {
                GameObject randomB = GameObject.FindWithTag("RandomBullet");
                Destroy(randomB);
            }
            // リセット
            if(timer >= EXPLOSION_END)
            {
                timer = 0;
                isTimerStart = false;
            }
        }
    }
}
