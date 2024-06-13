using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // �X�R�A�e�L�X�g�擾
    [SerializeField] private Text scoreText;

    private int score = 0;

    void Start()
    {
        // �\��
        scoreText.text = "SCORE: " + score;
    }

    // �X�R�A���Z�֐�
    public void AddScore(int score_)
    {
        score += score_;

        scoreText.text = "SCORE: " + score;
    }

}
