using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAGnome : IAEnemy
{
    

    public override Competence GetCompetence(Competence[] competences)
    {
        int randomIndex = Random.Range(0, competences.Length);

        return competences[randomIndex];
    }

    public override List<Unit> GetUnits(Competence competence, Competence[] competences)
    {
        List<Unit> Players = battleSysteme.playerUnitList;

        List<Unit> Enemys = battleSysteme.enemyUnitList;

        List<Unit> unitReturn = new();

        if(competence.friendly)
        {
            int randomIndex = Random.Range(0, Enemys.Count);

            unitReturn.Add(Enemys[randomIndex]);

            return unitReturn;
        }

        else
        {
           int randomIndex = Random.Range(0, Players.Count);

            unitReturn.Add(Players[randomIndex]);

            return unitReturn; 
        }
            
    }
}
