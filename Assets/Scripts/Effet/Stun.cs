using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Stun", menuName = "ScriptablesObjects/Boost/Stun")]
public class Stun : BoostEffect
{

    
    
    public override void ResetStat(Unit unitToReset)
    {
        unitToReset.stun = false;
    }

    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.stun = true;
    }
}
