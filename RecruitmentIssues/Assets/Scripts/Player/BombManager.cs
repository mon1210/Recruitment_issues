using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bomb�ɂ̂݃X�N���v�g������ƁA�ʃI�u�W�F�N�g�ŎQ�Ƃł��Ȃ��̂ŊǗ��X�N���v�g��p��
/// </summary>
public class BombManager : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    
    private Controller controllerScript;

    // �����������ǂ����𔻒f
    private bool isExplosion = false;

    // ���ˈʒu�����p�萔
    const float OFFSET_Y = 1.0f;
    // �����܂ł̎���
    const float EXPLOSION_TIMER = 1.5f;

    public bool IsExplosion { get => isExplosion; set => isExplosion = value; }

    void Start()
    {
        controllerScript = GetComponent<Controller>();

        // ������A��b��ɔ���
        Invoke("isDestroy", EXPLOSION_TIMER);
    }

    // ���������ǂ����𔻒f
    private void isDestroy()
    {
        // �ʂ̉ӏ���false�ɂ���
        isExplosion = true;
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
        }
    }

}
