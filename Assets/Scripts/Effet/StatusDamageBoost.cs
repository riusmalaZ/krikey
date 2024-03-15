using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatusDamageBoost", menuName = "ScriptablesObjects/Effect/StatusDamageBoost")]
public class StatusDamageBoost : Effect
{
    public int pourcentageAttNormal;

    public int pourcentageAttStatus;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {

        bool isStatus = unitReceveur.statuses.Count != 0;

        bool isBoost = unitReceveur.Boosts.Count != 0;

        if(isStatus || isBoost)
            unitReceveur.TakeDamge(unitLancer.damage,pourcentageAttStatus);
        
        else
            unitReceveur.TakeDamge(unitLancer.damage, pourcentageAttNormal);
    
    }
}
