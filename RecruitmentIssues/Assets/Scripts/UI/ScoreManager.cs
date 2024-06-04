using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 表示
        scoreText.text = "SCORE: " + score;
    }

    // スコア加算関数
    public void AddScore(int score_)
    {
        score += score_;

        scoreText.text = "SCORE: " + score;
    }

}
