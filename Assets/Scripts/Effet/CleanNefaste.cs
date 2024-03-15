using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CleanNefaste", menuName = "ScriptablesObjects/Effect/CleanNefaste")]
public class CleanNefaste : Effect
{

    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {

        if(unitReceveur.statuses.Count != 0)
            unitReceveur.statuses.RemoveAll(status => status.nefaste);

        if(unitReceveur.Boosts.Count != 0)
            unitReceveur.Boosts.RemoveAll( Boost => Boost.nefaste);
        
    }
}
