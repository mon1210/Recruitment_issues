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
