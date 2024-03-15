using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealCrystal", menuName = "ScriptablesObjects/Crystal/HealCrystal")]
public class HealCrystal : Effect
{
    public int Pourcentage;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.BeingHeal(unitReceveur.maxHP*Pourcentage/100);
    }
}
