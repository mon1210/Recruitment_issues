using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O�p�֐����g�p���Ċp�x�����߂�
/// </summary>
public class ChaseEnemyBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float chaseLimitTime = 0.0f;

    private GameObject player;
    // �ו�
    private float adjacent = 0.0f;
    // �Ε�
    private float opposite = 0.0f;
    // �Ε�
    private float hypotenuse = 0.0f;
    // �ǐՃ^�C�}�[
    private float chaseTimer = 0.0f;

    void Start()
    {
        // ���g��Prefab�Ȃ̂�Find���g�p
        player = GameObject.Find("Player");

        // �قƂ�ǂȂ����AchaseLimitTime��0�ȉ��̂Ƃ��G���[��f���̂Ōv�Z���Ă���
        if(chaseLimitTime <= 0.0f)
        {
            calculateTriangleSides();
        }
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
        adjacent = player.transform.position.x - this.transform.position.x;
        // �Ε�
        opposite = player.transform.position.y - this.transform.position.y;

        // �Ε� = ��(�ו�^2 + �Ε�^2)
        hypotenuse = Mathf.Sqrt(adjacent * adjacent + opposite * opposite);
    }

    private void move()
    {
        // �����x�N�g��(�e��)�𐳋K�����A�ړ����x��������
        transform.Translate(adjacent / hypotenuse * moveSpeed * Time.deltaTime, 
                            opposite / hypotenuse * moveSpeed * Time.deltaTime, 
                            0.0f
                            );
    }
}
