using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost
{
    public Unit Receveur;

    public BoostEffect boost;

    public int NbTours;

    private int NbToursTotal;

    public Boost(BoostEffect boost, int NbTours, Unit Receveur)
    {
        this.boost = boost;
        this.NbTours = NbTours;
        this.NbToursTotal = NbTours;
        this.Receveur = Receveur;
    }    

    public void Refresh()
    {
        NbTours = NbToursTotal;
    }

    public void TurnLess(Unit Receveur)
    {
        if(NbTours == 0)
            return;

        NbTours--;

        if(NbTours == 0)
        {
            boost.ResetStat(Receveur);
            Receveur.RemoveBoost(this);
        }
    }
}