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
    public Dictionary<Unit, List<GameObject> > unitsList = new();

    float chrono = 0;
    
    [SerializeField] GameObject Curseur;
    [SerializeField] List<Slider> sliders;

    [SerializeField] GameObject PlayerAttackButton;

    [SerializeField] GameObject EnnemisInterface;

    
    List<GameObject> EnnemisMort = new();


    float valMax;

    bool InSelection = false;    

    // Start is called before the first frame update
    void Awake()
    {
        state = BattleStates.Start;
        valMax = 100;
        PlayerAttackButton.SetActive(false);
        EnnemisInterface.SetActive(false);
        
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

        if(state == BattleStates.Win)
            EndBattle();
            
        
        
    }

    void SetupBattle()
    {
        indiceEnemy = 0; 
        indicePlayer = 0;

        foreach(GameObject P in PlayerPrefab)
        {
            GameObject PlayerGO = Instantiate(P, PlayerStation[indicePlayer]);
            PlayerUnit = PlayerGO.GetComponent<Unit>();
            unitsList.Add(PlayerUnit, new());
            PlayerUnit.BarreProg = sliders[indicePlayer];
            PlayerUnit.BarreProg.maxValue = valMax;
            unitsList[PlayerUnit].Add(PlayerGO);
            indicePlayer++;
        }
        
        foreach(GameObject E in EnemyPrefab)
        {
            GameObject EnemyGO = Instantiate(E, EnemyStation[indiceEnemy]);
            EnemyUnit = EnemyGO.GetComponent<Unit>();
            EnemyUnit.BarreProg = sliders[indicePlayer];
            EnemyUnit.BarreProg.maxValue = valMax;
            unitsList.Add(EnemyUnit, new());
            unitsList[EnemyUnit].Add(EnemyGO);
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
        
        foreach (Unit unit in unitsList.Keys)
        {
            //Debug.Log("Il est dedans");
            unit.Progression = unit.Progression + 1 * (unit.attackSpeed*Time.deltaTime); // Incrémente la valeur "progression" de l'unité
            unit.BarreProg.value = unit.Progression;
            //Debug.Log(unit.Progression);

            if (unit.Progression > valMax)
            {
                if(unit.Enemy)
                {
                    state = BattleStates.EnemyTurn;
                    EnemyUnit = unit;
                    return;
                }

                PlayerAttackButton.SetActive(true);
                state = BattleStates.PlayerTurn;
                PlayerUnit = unit;
                
                return;
            }
        }
    }

    void EnemyTurn()
    {
       //yield return new WaitForSeconds(1f);

        //Debug.Log("Enemy Turn");

        //yield return new WaitForSeconds(1f);

        //Debug.Log("aaaahhhh");
        //Debug.Log("aahhh *chie partout*");

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
            //Debug.Log("ah bah nan");
            return;
        }

        //Debug.Log("Appuie");
        
        EnnemisInterface.SetActive(true);
        PlayerAttackButton.SetActive(false);

        return;
    } 

    public void PlayerAttack(Unit ennemis)
    {
        bool isDead = ennemis.TakeDamge(PlayerUnit.damage);


        Debug.Log( ennemis.unitName + ": " + ennemis.currentHP);
        state = BattleStates.Neutre;
        PlayerUnit.Progression = 0;

        if(isDead)
        {
            EnnemisMort.Add(unitsList[ennemis][0]);
            Destroy(unitsList[ennemis][0]);
            Destroy(unitsList[ennemis][1]);
            
            unitsList.Remove(ennemis);

            if(EnnemisMort.Count == EnemyPrefab.Count)
                state = BattleStates.Win;
        }

        EnnemisInterface.SetActive(false);
        return;

    }

    void EndBattle()
    {
        Debug.Log("Gagné !");
    }


}

