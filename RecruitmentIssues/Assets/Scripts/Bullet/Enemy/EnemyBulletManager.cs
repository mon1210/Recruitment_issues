using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    // ƒ‰ƒ“ƒ_ƒ€‚É‚Î‚ç‚Ü‚©‚ê‚é’e
    private List<GameObject> randamBullets = new List<GameObject>();
    // Player‚ÉŒü‚©‚Á‚Ä”ò‚Ô’e
    //private List<GameObject> aimedBullets = new List<GameObject>();
    // ’ÇÕ’e
    //private List<GameObject> chaseBullets = new List<GameObject>();

    // ƒŠƒXƒg‚É’Ç‰Á     ˆø”‚Åí—Ş‚ğ”»’f
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

    // ƒŠƒXƒgã‚Ì’eíœ     ˆø”‚Åí—Ş‚ğ”»’f
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
