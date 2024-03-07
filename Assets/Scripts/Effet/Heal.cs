using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "ScriptablesObjects/Heal")]
public class Heal : Effect
{
    public int value;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.BeingHeal(value);
    }
}
