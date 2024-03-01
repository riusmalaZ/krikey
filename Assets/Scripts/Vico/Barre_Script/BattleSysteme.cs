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

    [HideInInspector]
    public Dictionary<Unit, List<GameObject> > unitsList = new();

    float chrono = 0;
    
    [SerializeField] GameObject Curseur;
    [SerializeField] List<Slider> sliders;

    [SerializeField] GameObject PlayerAttackButton;

    [SerializeField] GameObject EnnemisInterface;

    [SerializeField] GameObject PlayerSelectInterface;

    
    List<GameObject> EnnemisMort = new();

    List<GameObject> PlayerMort = new();

    List<Unit> EnemyUnitList = new();

    List<Unit> PlayerUnitList = new();

    

    float valMax;

    

    // Start is called before the first frame update
    void Awake()
    {
        state = BattleStates.Start;
        valMax = 100;
        PlayerAttackButton.SetActive(false);
        EnnemisInterface.SetActive(false);
        PlayerSelectInterface.SetActive(false);
        
        SetupBattle();
    }

    // Update is called once per frame
    void Update()
    {    
        switch (state)
        {   
            case BattleStates.Neutre:
                Jauge();
            break;

            case BattleStates.EnemyTurn:
                chrono += Time.deltaTime;
                if(chrono > 1f)
                    EnemyTurn();
            break;

            case BattleStates.Win:
            case BattleStates.Lose:
                EndBattle();
            break;   
        }
        
    }

    void SetupBattle()
    {
        for (int i = 0; i < PlayerPrefab.Count; i++)
        {
            GameObject PlayerGO = Instantiate(PlayerPrefab[i], PlayerStation[i]);
            PlayerUnit = PlayerGO.GetComponent<Unit>();
            unitsList.Add(PlayerUnit, new());
            PlayerUnit.BarreProg = sliders[i];
            PlayerUnit.BarreProg.maxValue = valMax;
            unitsList[PlayerUnit].Add(PlayerGO);
            PlayerUnitList.Add(PlayerUnit);
            
        
        }

        for (int i = 0; i < EnemyPrefab.Count; i++)
        {
            GameObject EnemyGO = Instantiate(EnemyPrefab[i], EnemyStation[i]);
            EnemyUnit = EnemyGO.GetComponent<Unit>();
            unitsList.Add(EnemyUnit, new());
            EnemyUnit.BarreProg = sliders[EnemyPrefab.Count + i];
            EnemyUnit.BarreProg.maxValue = valMax;
            unitsList[EnemyUnit].Add(EnemyGO);
            EnemyUnitList.Add(EnemyUnit);
        }

        state = BattleStates.Neutre;

        return;
    }

    void Jauge()
    {
        
        foreach (Unit unit in unitsList.Keys)
        {
            unit.Progress();

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
        int nb_Player = PlayerUnitList.Count;

        int randomIndex = Random.Range(0, nb_Player);

        PlayerUnit = PlayerUnitList[randomIndex];

        bool isDead = PlayerUnit.TakeDamge(EnemyUnit.damage);

        if(isDead)
        {
            PlayerMort.Add(unitsList[PlayerUnit][0]);

            Destroy(unitsList[PlayerUnit][0]);
            
            unitsList.Remove(PlayerUnit);

            

            if(PlayerMort.Count == nb_Player)
                state = BattleStates.Lose;
                chrono = 0;
                return;
        }

        EnemyUnit.Progression = 0;
        state = BattleStates.Neutre;
        chrono = 0;
        return; 

        
        
    }

    public void OnAttackButton()
    {
        if(state != BattleStates.PlayerTurn)
        {
            return;
        }
        
        EnnemisInterface.SetActive(true);
        PlayerAttackButton.SetActive(false);

        return;
    } 

    public void OnObjectButton()
    {
        if(state != BattleStates.PlayerTurn)
        {
            return;
        }
        
        PlayerSelectInterface.SetActive(true);
        PlayerAttackButton.SetActive(false);

        return;
    } 

    public void PlayerAttack(Unit ennemis)
    {
        if(PlayerUnit.toursRestant == 0)
        {
            PlayerMort.Add(unitsList[PlayerUnit][0]);

            Destroy(unitsList[PlayerUnit][0]);
            
            unitsList.Remove(PlayerUnit);

            

            if(PlayerMort.Count == PlayerUnitList.Count)
                state = BattleStates.Lose;
                return;
        }

        PlayerUnit.toursRestant--;

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

        if(PlayerUnit.toursRestant == 0)
        {
            PlayerMort.Add(unitsList[PlayerUnit][0]);

            Destroy(unitsList[PlayerUnit][0]);
            
            unitsList.Remove(PlayerUnit);

            

            if(PlayerMort.Count == PlayerUnitList.Count)
                state = BattleStates.Lose;
                return;
        }

        
        return;

    }

    void EndBattle()
    {
        if(state == BattleStates.Win)
            Debug.Log("Gagné !");
        
        else
            Debug.Log("Perdu !");
    }


}

