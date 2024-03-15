using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ToursBonusChance", menuName = "ScriptablesObjects/Effect/ToursbonusChance")]
public class ToursBonusChance : Effect
{
    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.toursRestant += Random.Range(1, 3);
    }
}
