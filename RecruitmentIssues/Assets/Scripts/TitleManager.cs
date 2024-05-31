using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnStart(InputAction.CallbackContext context)
    {
        // Spaceキー or pad下ボタン を押したら
        if (context.phase == InputActionPhase.Performed)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

}
