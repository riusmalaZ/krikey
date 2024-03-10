using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoostEffect : Effect
{

    public bool Stackable = false;

    public bool CanBeRefresh = true;

    public abstract void ResetStat(Unit unitToReset);
    public int Duration;

    public override void Apply(Unit unitLancer, Unit unitReceveur)
    {
        unitReceveur.ApplyBoost(this, Duration, unitLancer);
    }

}
