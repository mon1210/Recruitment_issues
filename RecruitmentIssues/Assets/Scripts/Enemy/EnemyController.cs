using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyCollider enemyCollider;
    // �ړ����x
    [SerializeField] private float moveSpeed = 0.0f;
    private float timer = 0.0f;

    // �ړ�����܂ł̃^�C��
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

    // �ړ��֐�
    private void move()
    {
        timer += Time.deltaTime;

        // �ŏ��͏�ړ�
        if (timer >= reverseMoveTime)
        {
            // �ړ��������]
            moveSpeed *= -1.0f;
            timer = 0.0f;
        }

        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
    }
}
