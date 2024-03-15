using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DamageBonus", menuName = "ScriptablesObjects/Crystal/DamageBonus")]
public class damageBonus : Effect
{
    public int Pourcentage;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.TakeDamge(unitReceveur.maxHP, Pourcentage);
    }
}
