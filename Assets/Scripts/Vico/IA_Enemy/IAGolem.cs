using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAGolem : IAEnemy
{

    Competence AttaquePrec;

    
    public override Competence GetCompetence(Competence[] competences)
    {
        List<Unit> Players = battleSysteme.playerUnitList;

        bool isBoost = false;

        Competence competence1 = null;

        List<Competence> competences1 = new();

        foreach (Unit unit in Players)
        {
            if(unit.Boosts.Count != 0)
                isBoost = true;
        }

        competences[0].Ponderation = 50;

        competences1.Add(competences[0]);

        if(competences[1].actualCooldown == 0)
            competences[1].Ponderation = 50;
        else    
            competences[1].Ponderation = 0;

        if(AttaquePrec == competences[1])
            competences[2].Ponderation = 100;
        else
            competences[2].Ponderation = 0;


        if(isBoost)
            competences[3].Ponderation = 100;
        else
            competences[3].Ponderation = 0;

        if(competence1 != null)
        {
            foreach (Competence item in competences)
            {
                item.Ponderation = 0;
            }
            return competence1;
        }
        else
        {
            int randomIndex = Random.Range(0, competences1.Count);

            foreach (Competence item in competences)
            {
                item.Ponderation = 0;
            }

            return competences1[randomIndex];
        }


    }

    public override List<Unit> GetUnits(Competence competence, Competence[] competences)
    {
        List<Unit> Players = battleSysteme.playerUnitList;

        List<Unit> unitReturn = new();

        if(competence == competences[0])
            return Players;

        else if(competence == competences[1])
        {
             unitReturn.Add(GetComponent<Unit>());
             return unitReturn;
        }

        else if(competence == competences[2])
        {
            return Players;
        }

        else
        {
            foreach (Unit unit in Players)
            {
                if(unit.Boosts.Count != 0)
                    unitReturn.Add(unit);
            }

            while (unitReturn.Count > 1)
            {
                // Générer un index aléatoire dans la plage des indices de la liste
                int randomIndex = Random.Range(0, unitReturn.Count);

                // Supprimer l'élément à l'index aléatoire de la liste
                unitReturn.RemoveAt(randomIndex);
            }

            return unitReturn;
        }
    }
}
