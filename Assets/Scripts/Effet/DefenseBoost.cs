using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DefenseBoost", menuName = "ScriptablesObjects/Boost/DefenseBoost")]
public class DefenseBoost : BoostEffect
{
    public int Pourcentage; 
    public override void Apply(Unit unitLancer,Unit unitReceveur )
    {
        unitReceveur.currentDef += unitReceveur.currentDef*(Pourcentage/100);
    }

    public override void ResetStat(Unit unitToReset)
    {
        unitToReset.currentDef = unitToReset.def;
    }
}
