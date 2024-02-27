using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public BattleStates state; 

    Unit PlayerUnit;
    Unit EnemyUnit;

    int indiceEnemy;

    int indicePlayer;

    List<Unit> unitsList = new();

    

    [SerializeField] List<Slider> sliders;

    float valMax;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleStates.Start;
        valMax = 100;
        SetupBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == BattleStates.Neutre)
            Jauge();

        else if (state == BattleStates.EnemyTurn)
            EnemyTurn();//StartCoroutine(EnemyTurn());

        
    }

    void SetupBattle()
    {
        indiceEnemy = 0; 
        indicePlayer = 0;

        foreach(GameObject P in PlayerPrefab)
        {
            GameObject PlayerGO = Instantiate(P, PlayerStation[indicePlayer]);
            PlayerUnit = PlayerGO.GetComponent<Unit>();
            unitsList.Add(PlayerUnit);
            PlayerUnit.BarreProg = sliders[indicePlayer];
            PlayerUnit.BarreProg.maxValue = valMax;
            indicePlayer++;
        }
        
        foreach(GameObject E in EnemyPrefab)
        {
            GameObject EnemyGO = Instantiate(E, EnemyStation[indiceEnemy]);
            EnemyUnit = EnemyGO.GetComponent<Unit>();
            EnemyUnit.BarreProg = sliders[indicePlayer];
            EnemyUnit.BarreProg.maxValue = valMax;
            unitsList.Add(EnemyUnit);
            indiceEnemy++;
            indicePlayer++;
        }

        indiceEnemy = 0; 
        indicePlayer = 0;

        state = BattleStates.Neutre;

        return;
    }

    void Jauge()
    {
        foreach (Unit unit in unitsList)
        {
            Debug.Log("Il est dedans");
            unit.Progression = unit.Progression + 1 * (unit.attackSpeed*Time.deltaTime); // Incrémente la valeur "progression" de l'unité
            unit.BarreProg.value = unit.Progression;
            Debug.Log(unit.Progression);

            if (unit.Progression > valMax)
            {
                if(unit.Enemy)
                {
                    state = BattleStates.EnemyTurn;
                    EnemyUnit = unit;
                    return;
                }
                state = BattleStates.PlayerTurn;
                PlayerUnit = unit;
                return;
            }
        }
    }

    public void EnemyTurn()
    {
        //yield return new WaitForSeconds(1f);

        Debug.Log("Enemy Turn");

        //yield return new WaitForSeconds(1f);

        Debug.Log("aaaahhhh");
        Debug.Log("aahhh *chie partout*");

        EnemyUnit.Progression = 0;
        state = BattleStates.Neutre;
        return;
        //yield break;
    }

    public void OnAttackButton()
    {
        if(state != BattleStates.PlayerTurn)
        {
            Debug.Log("ah bah nan");
            return;
        }

        Debug.Log("Appuie");
        PlayerUnit.Progression = 0;
        state = BattleStates.Neutre;
        return;
    } 
}

