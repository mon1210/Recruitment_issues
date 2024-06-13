using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // �K�C�h�e�L�X�g�擾
    [SerializeField] private Text text;

    // �t�F�[�h���x   (�傫���قǒx��)
    const float FADE_DURATION = 1.0f;

    void Start()
    {
        text = GetComponent<Text>();

        StartCoroutine(fadeText());
    }

    public void OnTitle(InputAction.CallbackContext context)
    {
        // Space�L�[ or pad���{�^�� ����������
        if (context.phase == InputActionPhase.Performed)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    // �e�L�X�g�̃t�F�[�h�A�E�g����
    private IEnumerator fadeText()
    {
        while (true)
        {
            // sin���g�p���邱�ƂŒl�������I�ɕω�������
            // 1�𑫂���2�Ŋ��邱�Ƃ�[-1�`1]�̎�����[0�`1]�ɕϊ�����
            float Alpha = (Mathf.Sin(Time.time * Mathf.PI / FADE_DURATION) + 1.0f) / 2.0f;

            Color NewColor = text.color;
            NewColor.a = Alpha;
            text.color = NewColor;

            // ���̃t���[���܂ő҂�
            yield return null;
        }
    }
}
