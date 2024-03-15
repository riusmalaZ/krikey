using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Envoutant", menuName = "ScriptablesObjects/Effect/Envoutant")]
public class Envoutant : Effect
{
    
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        if(unitReceveur.Progression - 20 <= 0)
            unitReceveur.Progression = 0;

        else
            unitReceveur.Progression = unitReceveur.Progression - 20;
    }
}

