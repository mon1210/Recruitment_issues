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

    // リスト上の弾削除関数判別
    public int DestroyAllBullets(BulletKind bullet_kind)
    {
        return bullet_kind switch
        {
            BulletKind.Random => DestroyAllBulletLists(randomBullets),
            BulletKind.Aimed => DestroyAllBulletLists(aimedBullets),
            BulletKind.Chase => DestroyAllBulletLists(chaseBullets),
            _ => 0,
        };
    }

    // リスト上の弾削除
    private int DestroyAllBulletLists(List<GameObject> bullets)
    {
        int DestroyedCount = bullets.Count;
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        bullets.Clear();
        return DestroyedCount;
    }
}
