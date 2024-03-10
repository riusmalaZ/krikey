using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatusDamageBoost", menuName = "ScriptablesObjects/StatusDamageBoost")]
public class StatusDamageBoost : Effect
{
    public int pourcentage;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {

        bool isStatus = unitReceveur.statuses.Count != 0;

        if(isStatus)
            unitReceveur.TakeDamge((int)(unitLancer.damage * (1 + pourcentage)));
        
        else
            unitReceveur.TakeDamge(unitLancer.damage);
    
    }
}
