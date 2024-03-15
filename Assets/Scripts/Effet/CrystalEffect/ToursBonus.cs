using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToursBonus", menuName = "ScriptablesObjects/Crystal/ToursBonus")]
public class ToursBonus : Effect
{
    public int Tours;
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.toursRestant += Tours; 
    }
}
