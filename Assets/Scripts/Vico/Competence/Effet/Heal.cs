using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "ScriptablesObjects/Heal")]
public class Heal : Effect
{
    public override void Apply(Unit unitLancer, Unit unitReceveur, int valHeal = 0)
    {
        unitReceveur.BeingHeal(valHeal);
    }
}
