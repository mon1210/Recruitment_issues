using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bomb�ɂ̂݃X�N���v�g������ƁA�ʃI�u�W�F�N�g�ŎQ�Ƃł��Ȃ��̂ŊǗ��X�N���v�g��p��
/// Bomb���Ƃ����Ɏ��g�������Ă��܂��̂ŁA�G�̒e���폜������Ȃ�
/// </summary>
public class BombManager : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    
    private Controller controllerScript;
    private float timer = 0;
    private bool isTimerStart = false;

    // ���ˈʒu�����p�萔
    const float OFFSET_Y = 1.0f;
    // �����܂ł̎���
    const float EXPLOSION_START = 1.5f;
    // �����I������
    const float EXPLOSION_END = 2.0f;

    void Start()
    {
        controllerScript = GetComponent<Controller>();
    }

    void Update()
    {
        if(controllerScript.IsBombInstantiate)
        {
            // �ʒu����(���̏ꍇ��transform.position��Player)
            bombPrefab.transform.position = new Vector3(transform.position.x + OFFSET_Y, transform.position.y, transform.position.z);

            // ����
            Instantiate(bombPrefab);

            controllerScript.IsBombInstantiate = false;

            isTimerStart = true;
        }

        // ����
        if (isTimerStart)
        {
            timer += Time.deltaTime;
            // 0.5�b�ԁA�G�̒e�폜
            if(timer >= EXPLOSION_START)
            {
                GameObject randomB = GameObject.FindWithTag("RandomBullet");
                Destroy(randomB);
            }
            // ���Z�b�g
            if(timer >= EXPLOSION_END)
            {
                timer = 0;
                isTimerStart = false;
            }
        }
    }
}
