using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBombText : MonoBehaviour
{
    [SerializeField] private Text currentBombText;
    [SerializeField] private GameObject player;
    private Controller controllerScript;

    void Start()
    {
        controllerScript = player.GetComponent<Controller>();
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
