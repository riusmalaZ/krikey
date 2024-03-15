using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Speed", menuName = "ScriptablesObjects/Effect/Speed")]
public class Speed : BoostEffect
{

    public int pourcentage;

    public int negatif =1;
    
    public override void ResetStat(Unit unitToReset)
    {
        unitToReset.currentAttSpeed = unitToReset.attackSpeed;
    }

    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.currentAttSpeed += negatif*unitReceveur.currentAttSpeed*(pourcentage/100);
    }
}
