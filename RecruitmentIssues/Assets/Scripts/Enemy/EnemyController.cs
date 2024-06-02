using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyCollider enemyCollider;
    // ˆÚ“®‘¬“x
    [SerializeField] private float moveSpeed = 0.0f;
    private float timer = 0.0f;

    // ˆÚ“®”»’è‚Ü‚Å‚Ìƒ^ƒCƒ€
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

    // ˆÚ“®ŠÖ”
    private void move()
    {
        timer += Time.deltaTime;

        // Å‰‚ÍãˆÚ“®
        if (timer >= reverseMoveTime)
        {
            // ˆÚ“®•ûŒü”½“]
            moveSpeed *= -1.0f;
            timer = 0.0f;
        }

        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
}
