using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private EnemyController enemyControllerScript;

    void Start()
    {
        
    }

    void Update()
    {
        // Player死亡時、Enemyのコールチン(弾生成)終了
        if(!player.activeInHierarchy)
        {
            enemyControllerScript.StopAllCoroutines();
        }
    }
}
