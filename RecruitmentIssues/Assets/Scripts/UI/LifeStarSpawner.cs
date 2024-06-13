using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStarSpawner : MonoBehaviour
{
    // lifeStars画像取得
    [SerializeField] private GameObject[] lifeStars;

    // 残機UI更新
    public void UpdateLifeStarsUI(int life)
    {
        // 一度消す
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
