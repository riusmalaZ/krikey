using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Provocation", menuName = "ScriptablesObjects/Effect/Provocation")]
public class Provocation : Effect
{
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitLancer.isProvoc = true;
    }
}
