using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStarSpawner : MonoBehaviour
{
    // lifeStars�摜�擾
    [SerializeField] private GameObject[] lifeStars;

    // �c�@UI�X�V
    public void UpdateLifeStarsUI(int life)
    {
        // ��x����
        for (int i = 0; i < lifeStars.Length; i++)
        {
            lifeStars[i].SetActive(false);
        }

        for (int i = 0; i < life; i++) 
        {
            lifeStars[i].SetActive(true);
        }
    }
}
