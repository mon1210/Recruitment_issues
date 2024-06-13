using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBulletText : MonoBehaviour
{
    // �c�e���e�L�X�g�擾
    [SerializeField] private Text currentBulletText;
    // Player�擾
    [SerializeField] private GameObject player;

    private PlayerController controllerScript;

    void Start()
    {
        controllerScript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        currentBulletText.text = "BULLET: " + controllerScript.CurrentBullet;
    }
}
