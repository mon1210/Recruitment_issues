using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBulletText : MonoBehaviour
{
    // 残弾数テキスト取得
    [SerializeField] private Text currentBulletText;
    // Player取得
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
