using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private Controller controllerScript;

    void Start()
    {
        controllerScript = GetComponent<Controller>();
    }

    void Update()
    {
        // Player非表示時にエンジンも非表示
        if (controllerScript.HitPoint <= 0)
        {
            explosion.SetActive(true);
        }

        // アニメーション終了時にfalse     Controllerに終了フラグを返す
    }
}
