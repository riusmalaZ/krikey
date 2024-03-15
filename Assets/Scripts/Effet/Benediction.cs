using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Benediction", menuName = "ScriptablesObjects/Effect/Benediction")]
public class Benediction : Effect
{
    
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.BeingHeal((unitReceveur.maxHP - unitReceveur.currentHP)/2);
    }
}

