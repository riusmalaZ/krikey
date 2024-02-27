using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleStates {Start, PlayerTurn, EnemyTurn, Lose, Win, Neutre}

public class BattleSysteme : MonoBehaviour
{
    [Header("Placement Joueur/Enemy")]
    [SerializeField]
    List<Transform> PlayerStation = new();

    [SerializeField]
    List<Transform> EnemyStation = new();

    [Header("Prefab Joueur/Enenmy")]
    [SerializeField]
    List<GameObject> PlayerPrefab = new();

    [SerializeField]
    List<GameObject> EnemyPrefab = new();

    [Header("Etat du Combat")]
    public BattleState state; 

    Unit PlayerUnit;
    Unit EnemyUnit;

    int indiceEnemy;

    int indicePlayer;

    List<Unit> unitsPlayer = new();

    List<Unit> unitsEnemy = new();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupBattle()
    {
        indiceEnemy = 0; 
        indicePlayer = 0;

        foreach(GameObject P in PlayerPrefab)
        {
            GameObject PlayerGO = Instantiate(P, PlayerStation[indicePlayer]);
            PlayerUnit = PlayerGO.GetComponent<Unit>();
            unitsPlayer.Add(PlayerUnit);
            indicePlayer++;
        }
        
        foreach(GameObject E in EnemyPrefab)
        {
            GameObject EnemyGO = Instantiate(E, EnemyStation[indiceEnemy]);
            EnemyUnit = EnemyGO.GetComponent<Unit>();
            unitsEnemy.Add(EnemyUnit);
            indiceEnemy++;
        }

        indiceEnemy = 0; 
        indicePlayer = 0;

        return;
    }
}
