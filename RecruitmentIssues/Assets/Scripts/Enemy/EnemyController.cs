using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyCollider enemyCollider;
    // 移動速度
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private GameObject randomBulletPrefab;
    private float moveTimer = 0.0f;

    // 移動判定までのタイム
    const float REVERSE_MOVE_TIME = 3.0f;
    // 発射間隔定数
    const float FIRE_INTERVAL = 0.5f;
    // 弾発射位置調整用定数
    const float BULLET_OFFSET_X = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<EnemyCollider>();

        // ランダムな弾の生成を開始
        StartCoroutine(SpawnRandomBullet());
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
        moveTimer += Time.deltaTime;

        // 最初は上移動
        if (moveTimer >= REVERSE_MOVE_TIME)
        {
            // 移動方向反転
            moveSpeed *= -1.0f;
            moveTimer = 0.0f;
        }

        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    // ランダムな弾を生成する
    private IEnumerator SpawnRandomBullet()
    {
        while (true) 
        { 
            randomBulletPrefab.transform.position = new Vector3(transform.position.x - BULLET_OFFSET_X, transform.position.y, transform.position.z); ;

            // 生成
            Instantiate(randomBulletPrefab);

            // インターバルを待つ
            yield return new WaitForSeconds(FIRE_INTERVAL);
        }
    }
}
