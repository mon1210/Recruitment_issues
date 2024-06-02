using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyCollider enemyCollider;
    // 移動速度
    [SerializeField] private float moveSpeed = 0.0f;
    private float timer = 0.0f;

    // 移動判定までのタイム
    const float reverseMoveTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<EnemyCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCollider.IsMoveAble)
        {
            move();
        }
    }

    // 移動関数
    private void move()
    {
        timer += Time.deltaTime;

        // 最初は上移動
        if (timer >= reverseMoveTime)
        {
            // 移動方向反転
            moveSpeed *= -1.0f;
            timer = 0.0f;
        }

        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
}
