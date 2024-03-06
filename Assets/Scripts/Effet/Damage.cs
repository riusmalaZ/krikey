using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Damage", menuName = "ScriptablesObjects/Damage")]
public class Damage : Effect
{
    public override void Apply(Unit unitLancer, Unit unitReceveur, int valHeal = 0)
    {
        unitReceveur.TakeDamge(unitLancer.damage);
    }
}
