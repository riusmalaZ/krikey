using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    [HideInInspector]
    public List<Unit> unitsList = new();

    float chrono = 0;
    
    [SerializeField] GameObject Curseur;
    [SerializeField] List<Slider> sliders;

    [SerializeField] GameObject PlayerUI;

    float valMax;

    bool InSelection = false;    

    // Start is called before the first frame update
    void Awake()
    {
        state = BattleStates.Start;
        valMax = 100;
        PlayerUI.SetActive(false);
        
        SetupBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == BattleStates.Neutre)
            Jauge();

        else if (state == BattleStates.EnemyTurn){
            chrono += Time.deltaTime;
            if(chrono > 1f)
                EnemyTurn();
        }

    
        
        
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

                PlayerUI.SetActive(true);
                state = BattleStates.PlayerTurn;
                PlayerUnit = unit;
                
                return;
            }
        }
    }

    void EnemyTurn()
    {
       //yield return new WaitForSeconds(1f);

        Debug.Log("Enemy Turn");

        //yield return new WaitForSeconds(1f);

        Debug.Log("aaaahhhh");
        Debug.Log("aahhh *chie partout*");

        EnemyUnit.Progression = 0;
        state = BattleStates.Neutre;
        chrono = 0;
        return; 

        //StopCoroutine(EnemyTurn());

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
        PlayerUI.SetActive(false);

        return;
    } 

    void SelectionEnemy()
    {
        Instantiate(Curseur, EnemyStation[0]);
        
    }
}

