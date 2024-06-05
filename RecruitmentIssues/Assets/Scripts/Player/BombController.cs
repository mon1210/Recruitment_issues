using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    // �_�Ŏ���
    [SerializeField] private float blinkInterval = 0.0f;

    private SpriteRenderer spriteRenderer;
    private float timer = 0.0f;

    // �����܂ł̎���
    const float EXPLOSION_TIMER = 1.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ������A��b��ɔ���
        Invoke("destroy", EXPLOSION_TIMER);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        blinking();
    }

    // �_�Ŋ֐�
    private void blinking()
    {
        // �����������o�߂�����
        timer += Time.deltaTime;

        // timer�`blinkInterval�̊ԂŌJ��Ԃ��l���擾
        float repeatValue = Mathf.Repeat((float)timer, blinkInterval);

        // �C���^�[�o���̔����ȏ�̎��͕\��
        if(repeatValue >= blinkInterval * 0.5f)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }

    }
}
