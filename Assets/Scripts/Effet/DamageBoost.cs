using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DamageBoost", menuName = "ScriptablesObjects/Boost/DamageBoost")]
public class DamageBoost : BoostEffect
{

    public int pourcentage;

    public int negatif = 1;
    
    public override void ResetStat(Unit unitToReset)
    {
        unitToReset.currentDamage = unitToReset.damage;
    }

    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.currentDamage += negatif*unitReceveur.currentAttSpeed*(pourcentage/100);
    }
}
