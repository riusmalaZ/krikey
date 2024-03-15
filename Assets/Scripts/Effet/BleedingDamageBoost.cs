using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BleedingDamageBoost", menuName = "ScriptablesObjects/Boost/BleedingDamageBoost")]
public class BleedingDamageBoost : Effect
{
    public int pourcentageAttNormal;

    public int pourcentageAttBleed;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {

        if(unitReceveur.statuses.Count != 0)
        {
           unitReceveur.TakeDamge(unitLancer.damage,pourcentageAttBleed);
        }
        else
            unitReceveur.TakeDamge(unitLancer.damage, pourcentageAttNormal);
    
    }
}
