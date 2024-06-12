using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBombText : MonoBehaviour
{
    // ボムの残弾数テキスト取得
    [SerializeField] private Text currentBombText;
    // Player取得
    [SerializeField] private GameObject player;

    private PlayerController controllerScript;

    void Start()
    {
        controllerScript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        // 残弾0の時テキストを赤に
        if(controllerScript.CurrentBomb <= 0) 
        { 
            currentBombText.color = Color.red; 
        }

        currentBombText.text = "BOMB: " + controllerScript.CurrentBomb;
    }
}
