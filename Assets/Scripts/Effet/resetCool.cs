using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "resetCool", menuName = "ScriptablesObjects/Effect/resetCool")]
public class resetCool : Effect
{
    public int value;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        foreach (Competence item in unitReceveur.competences)
        {
            item.actualCooldown = 0;
        }
    }
}
