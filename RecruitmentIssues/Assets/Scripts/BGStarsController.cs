using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGStarsController : MonoBehaviour
{
    [SerializeField] private float resetPositionX = 0.0f;
    [SerializeField] private float moveSpeed = 2.0f;
    private Vector3 startPosition = Vector3.zero;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);

        // 座標リセット
        if(transform.position.x < resetPositionX) 
        {
            transform.position = startPosition;
        }
    }
}
