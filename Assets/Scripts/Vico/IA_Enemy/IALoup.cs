using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IALoup : IAEnemy
{


    public override Competence GetCompetence(Competence[] competences)
    {
        List<Unit> Players = battleSysteme.playerUnitList;

        bool isBleed = false;

        Competence competence1 = null;

        List<Competence> competences1 = new();

        foreach (Unit unit in Players)
        {
            if(unit.statuses.Count != 0)
                isBleed = true;
        }

        foreach (Unit unit in Players)
        {
            if(unit.isProvoc)
            {
                competence1 = competences[0];
                return competence1;
            }
        }


        if(competences[3].actualCooldown == 0)
        {
            competences[3].Ponderation = 100;
        }
        else
        {
            competences[3].Ponderation = 0;  
        }

        if(isBleed)
        {
            competences[2].Ponderation = 100;
        }
        else
        {
            competences[2].Ponderation = 0;
        }

        if(competences[3].Ponderation == 0 && competences[2].Ponderation == 0)
        {
            competences[1].Ponderation = 50;
        }
        else
        {
            competences[1].Ponderation = 0;
        }

        if(competences[3].Ponderation == 0 && competences[2].Ponderation == 0)
        {
            competences[0].Ponderation = 50;
        }
        else if(competences[3].Ponderation == 100)
        {
            competences[0].Ponderation = 0;
        }
        else if(competences[2].Ponderation == 100)
        {
            competences[0].Ponderation = 0;
        }
        else
        {
            competences[0].Ponderation = 0;
        }

        foreach (Competence competence in competences)
        {
            if(competence.Ponderation == 100)
                competence1 = competence;

            else if(competence.Ponderation == 50)
                competences1.Add(competence);
        }

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
        List<Unit> unitReturn = new();

        List<Unit> Players = battleSysteme.playerUnitList;

        foreach (Unit unit in Players)
        {
                if(unit.isProvoc)
                {
                    unitReturn.Add(unit);
                    unit.isProvoc = false;
                    return unitReturn;
                }
        }

        if(competence == competences[3])
        {
            unitReturn.Add(GetComponent<Unit>());
        }
        else if(competence == competences[2])
        {
            foreach (Unit unit in Players)
            {
                if(unit.statuses.Count != 0)
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

        else if(competence == competences[1])
        {
            return Players;
        }

        else if(competence == competences[0])
        {
            Unit hp = Players[0];

            for (int i = 0; i < Players.Count; i++)
            {
                if(Players[i].currentHP < hp.currentHP)
                    hp = Players[i];
            }

            unitReturn.Add(hp);

            return unitReturn;
        }

        return unitReturn;
    }

    
}
