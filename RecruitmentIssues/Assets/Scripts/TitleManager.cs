using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnStart(InputAction.CallbackContext context)
    {
        // Space�L�[ or pad���{�^�� ����������
        if (context.phase == InputActionPhase.Performed)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

}
