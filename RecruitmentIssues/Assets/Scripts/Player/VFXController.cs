using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private Controller controllerScript;

    void Start()
    {
        controllerScript = GetComponent<Controller>();
    }

    void Update()
    {
        // Player��\�����ɃG���W������\��
        if (controllerScript.HitPoint <= 0)
        {
            explosion.SetActive(true);
        }

        // �A�j���[�V�����I������false     Controller�ɏI���t���O��Ԃ�
    }
}
