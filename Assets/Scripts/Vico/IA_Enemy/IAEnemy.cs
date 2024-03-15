using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAEnemy : MonoBehaviour
{
    public BattleSysteme battleSysteme;
    // Start is called before the first frame update
    public abstract Competence GetCompetence(Competence[] competences);

    public abstract List<Unit> GetUnits(Competence competence, Competence[] competences);
}
