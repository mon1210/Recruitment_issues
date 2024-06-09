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
        // Player���S���AEnemy�̃R�[���`��(�e����)�I��
        if(!player.activeInHierarchy)
        {
            enemyControllerScript.StopAllCoroutines();
        }
    }
}
