using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : Effect
{


    public bool Stackable = false;

    public bool CanBeRefresh = true; 

    public abstract void Resolve(Unit unitLancer,Unit unitReceveur);

    public int duration;

    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.ApplyStatus(this, duration, unitLancer);
    }
    
}
