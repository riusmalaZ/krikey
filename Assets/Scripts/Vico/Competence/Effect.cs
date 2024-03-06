using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    
    public abstract void Apply(Unit unitLancer, Unit unitReceveur, int valHeal = 0);
    
}