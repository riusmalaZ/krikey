using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{

    public Unit Attaquant;
    public StatusEffect effect;

    public int NbTours;

    private int NbToursTotal;
    public Status(StatusEffect effect, int NbTours,  Unit Attaquant)
    {
        this.effect = effect;
        this.NbTours = NbTours;
        this.NbToursTotal = NbTours;
        this.Attaquant = Attaquant;
    }

    public void Refresh()
    {
        NbTours = NbToursTotal;
    }

    public void Resolve(Unit Receveur)
    {
        if(NbTours == 0)
            return;
        
        effect.Resolve(Attaquant, Receveur);
        NbTours--;
        
        if(NbTours == 0)
            Receveur.RemoveStatus(this);

    }
}
