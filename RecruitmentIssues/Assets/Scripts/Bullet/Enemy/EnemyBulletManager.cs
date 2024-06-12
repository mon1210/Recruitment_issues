using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    // ランダムにばらまかれる弾
    private List<GameObject> randomBullets = new List<GameObject>();
    // Playerに向かって飛ぶ弾
    private List<GameObject> aimedBullets = new List<GameObject>();
    // ホーミング弾
    private List<GameObject> chaseBullets = new List<GameObject>();

    public enum BulletKind
    { 
        Default = -1,
        Random,
        Aimed,
        Chase
    }

    // リストに追加     引数で種類を判断
    public void AddBulletList(BulletKind bullet_kind, GameObject bullet_)
    {
        switch (bullet_kind)
        {
            case BulletKind.Random:
                randomBullets.Add(bullet_);
                break;
            case BulletKind.Aimed:
                aimedBullets.Add(bullet_);
                break;
            case BulletKind.Chase:
                chaseBullets.Add(bullet_);
                break;
            default:
                break;
        }
    }

    // リスト上の弾削除     引数で種類を判断
    public int DestroyAllBullets(BulletKind bullet_kind)
    {
        int DestroyedCount = 0;
        switch (bullet_kind)
        {
            case BulletKind.Random:
                DestroyedCount = randomBullets.Count;
                foreach (GameObject bullet in randomBullets)
                {
                    Destroy(bullet);
                }
                randomBullets.Clear();
                return DestroyedCount;

            case BulletKind.Aimed:
                DestroyedCount = aimedBullets.Count;
                foreach (GameObject bullet in aimedBullets)
                {
                    Destroy(bullet);
                }
                aimedBullets.Clear();
                return DestroyedCount;

            case BulletKind.Chase:
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
