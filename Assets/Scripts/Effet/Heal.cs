using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "ScriptablesObjects/Effect/Heal")]
public class Heal : Effect
{
    public int value;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.BeingHeal(value);
    }
}
