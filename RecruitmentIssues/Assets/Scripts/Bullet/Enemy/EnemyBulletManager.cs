using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    // �����_���ɂ΂�܂����e
    private List<GameObject> randamBullets = new List<GameObject>();
    // Player�Ɍ������Ĕ�Ԓe
    //private List<GameObject> aimedBullets = new List<GameObject>();
    // �ǐՒe
    //private List<GameObject> chaseBullets = new List<GameObject>();

    // ���X�g�ɒǉ�     �����Ŏ�ނ𔻒f
    public void AddBulletList(string bullet_kind, GameObject bullet_)
    {
        switch (bullet_kind)
        {
            case "Random":
                randamBullets.Add(bullet_);
                break;
            //case "Aimed":
            //    aimedBullets.Add(bullet_);
            //    break;
            //case "Chase":
            //    chaseBullets.Add(bullet_);
            //    break;
            default:
                break;
        }
    }

    // ���X�g��̒e�폜     �����Ŏ�ނ𔻒f
    public int DestroyAllBullets(string bullet_kind)
    {
        int DestroyedCount = 0;
        switch (bullet_kind)
        {
            case "Random":
                DestroyedCount = randamBullets.Count;
                foreach (GameObject bullet in randamBullets)
                {
                    Destroy(bullet);
                }
                randamBullets.Clear();
                return DestroyedCount;

            //case "Aimed":
            //    DestroyedCount = aimedBullets.Count;
            //    foreach (GameObject bullet in aimedBullets)
            //    {
            //        Destroy(bullet);
            //    }
            //    aimedBullets.Clear();
            //    return DestroyedCount;

            //case "Chase":
            //    DestroyedCount = chaseBullets.Count;
            //    foreach (GameObject bullet in chaseBullets)
            //    {
            //        Destroy(bullet);
            //    }
            //    chaseBullets.Clear();
            //    return DestroyedCount;

            default:
                return 0;
        }
    }
}
