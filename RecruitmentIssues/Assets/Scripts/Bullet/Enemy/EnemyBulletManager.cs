using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    // ランダムにばらまかれる弾
    private List<GameObject> randamBullets = new List<GameObject>();
    // Playerに向かって飛ぶ弾
    private List<GameObject> aimedBullets = new List<GameObject>();
    // ホーミング弾
    private List<GameObject> chaseBullets = new List<GameObject>();

    // リストに追加     引数で種類を判断
    public void AddBulletList(string bullet_kind, GameObject bullet_)
    {
        switch (bullet_kind)
        {
            case "Random":
                randamBullets.Add(bullet_);
                break;
            case "Aimed":
                aimedBullets.Add(bullet_);
                break;
            case "Chase":
                chaseBullets.Add(bullet_);
                break;
            default:
                break;
        }
    }

    // リスト上の弾削除     引数で種類を判断
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

            case "Aimed":
                DestroyedCount = aimedBullets.Count;
                foreach (GameObject bullet in aimedBullets)
                {
                    Destroy(bullet);
                }
                aimedBullets.Clear();
                return DestroyedCount;

            case "Chase":
                DestroyedCount = chaseBullets.Count;
                foreach (GameObject bullet in chaseBullets)
                {
                    Destroy(bullet);
                }
                chaseBullets.Clear();
                return DestroyedCount;

            default:
                return 0;
        }
    }
}
