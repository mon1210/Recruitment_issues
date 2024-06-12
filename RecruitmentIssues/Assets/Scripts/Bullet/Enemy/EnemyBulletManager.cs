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

    // ���X�g��̒e�폜�֐�����
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

    // ���X�g��̒e�폜
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
