using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBombText : MonoBehaviour
{
    // �{���̎c�e���e�L�X�g�擾
    [SerializeField] private Text currentBombText;
    // Player�擾
    [SerializeField] private GameObject player;

    private PlayerController controllerScript;

    void Start()
    {
        controllerScript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        // �c�e0�̎��e�L�X�g��Ԃ�
        if(controllerScript.CurrentBomb <= 0) 
        { 
            currentBombText.color = Color.red; 
        }

        currentBombText.text = "BOMB: " + controllerScript.CurrentBomb;
    }
}
