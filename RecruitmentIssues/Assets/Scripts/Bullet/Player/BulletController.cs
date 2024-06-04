using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // 移動速度
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
        // Z軸で-90度回転しているので、Y軸方向にプラスで右移動
        transform.Translate(0.0f, moveSpeed * Time.deltaTime, 0.0f);
    }

}
