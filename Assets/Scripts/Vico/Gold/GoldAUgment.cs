using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAUgment : MonoBehaviour
{

    public Gold gold;
    // Start is called before the first frame update
    public void ToursGold()
    {
        gold.goldGagne += 5;
    }

    public void MobKillGold()
    {
        gold.goldGagne += 5;
    }

    public void BossKill()
    {
        gold.goldGagne += 50;
    }

    public void PersoEnVie()
    {
        gold.goldGagne += 5;
    }

    public void ObjectUse()
    {
        gold.goldGagne += 10;
    }

    public void NbCrystalBrise()
    {
        gold.goldGagne += 5;
    }
}
