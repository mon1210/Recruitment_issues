using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O�p�֐����g�p���Ċp�x�����߂�
/// </summary>
public class ChaseEnemyBullet : MonoBehaviour
{
    // �ړ����x
    [SerializeField] private float moveSpeed = 0.0f;
    // �ǐՂ���ő厞��     �ŏ�0.1f�ɂ��Ȃ��ƃG���[��f��
    [SerializeField, Min(0.1f)] private float chaseLimitTime = 0.1f;

    private GameObject player;

    // �ǐՃ^�C�}�[
    private float chaseTimer = 0.0f;

    private Vector2 moveVec = Vector2.zero;

    void Start()
    {
        // ���g��Prefab�Ȃ̂�Find���g�p
        player = GameObject.Find("Player");
    }

    void Update()
    {
        chaseTimer += Time.deltaTime;
        // �ǐՎ��ԓ�
        if(chaseTimer < chaseLimitTime)
        {
            // ���ꂼ��̕ӂ��v�Z
            calculateTriangleSides();
        }

        // �ړ�
        move();
    }

    // �ӌv�Z
    private void calculateTriangleSides()
    {
        // �ו�
        moveVec.x = player.transform.position.x - this.transform.position.x;
        // �Ε�
        moveVec.y = player.transform.position.y - this.transform.position.y;

        // �Ε� = ��(�ו�^2 + �Ε�^2)
        float Hypotenuse = Mathf.Sqrt(moveVec.x * moveVec.x + moveVec.y * moveVec.y);

        // �����x�N�g��(�e��)�𐳋K�����A�ړ����x��������
        moveVec = new Vector2(moveVec.x / Hypotenuse * moveSpeed * Time.deltaTime,
                              moveVec.y / Hypotenuse * moveSpeed * Time.deltaTime);

    }

    private void move()
    {
        transform.Translate(moveVec);
    }
}
