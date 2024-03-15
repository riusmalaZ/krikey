using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Damage", menuName = "ScriptablesObjects/Effect/Damage")]
public class Damage : Effect
{
    public float pourcentage;

    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.TakeDamge(unitLancer.damage, pourcentage);
    
    }
}
