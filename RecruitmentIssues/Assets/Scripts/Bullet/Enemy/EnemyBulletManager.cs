using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    private List<GameObject> randamBullets = new List<GameObject>();
    //private List<GameObject> aimedBullets = new List<GameObject>();
    //private List<GameObject> chaseBullets = new List<GameObject>();

    public void AddRandomList(GameObject randomBullet_)
    {
        randamBullets.Add(randomBullet_);
    }

    //public void AddAimedList(GameObject aimedBullet_)
    //{
    //    aimedBullets.Add(randomBullet_);
    //}

    //public void AddChaseList(GameObject chaseBullet_)
    //{
    //    chaseBullets.Add(randomBullet_);
    //}

    // ÉäÉXÉgè„ÇÃíeçÌèú
    public int DestroyAllBullets()
    {
        int destroyedCount = randamBullets.Count;
        foreach (GameObject bullet in randamBullets)
        {
            Destroy(bullet);
        }
        randamBullets.Clear();
        return destroyedCount;
    }
}
