using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletController : MonoBehaviour
{
    // �ړ����x
    [SerializeField] private int moveSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        transform.Translate(0.0f, moveSpeed * Time.deltaTime, 0.0f);
    }

}
