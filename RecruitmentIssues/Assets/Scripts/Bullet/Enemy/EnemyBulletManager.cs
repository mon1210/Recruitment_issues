using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    // �����_���ɂ΂�܂����e
    private List<GameObject> randomBullets = new List<GameObject>();
    // Player�Ɍ������Ĕ�Ԓe
    private List<GameObject> aimedBullets = new List<GameObject>();
    // �z�[�~���O�e
    private List<GameObject> chaseBullets = new List<GameObject>();

    public enum BulletKind
    { 
        Default = -1,
        Random,
        Aimed,
        Chase
    }

    // ���X�g�ɒǉ�     �����Ŏ�ނ𔻒f
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

    // ���X�g��̒e�폜     �����Ŏ�ނ𔻒f
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
