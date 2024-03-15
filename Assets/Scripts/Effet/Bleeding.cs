using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bleeding", menuName = "ScriptablesObjects/Status/Bleeding")]
public class Bleeding : StatusEffect
{
    public int Pourcentage; 
    public override void Resolve(Unit unitLancer,Unit unitReceveur)
    {
        unitReceveur.TakeDamge(unitReceveur.maxHP, Pourcentage);
    }

    
}
