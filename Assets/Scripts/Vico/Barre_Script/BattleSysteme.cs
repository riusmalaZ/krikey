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

    public Unit playerUnit
    {
        get { return PlayerUnit; }
        set { PlayerUnit = value; }
    }
    Unit EnemyUnit;

    [HideInInspector]
    public Dictionary<Unit, List<GameObject> > unitsList = new();

    float chrono = 0;
    
    
    [SerializeField] List<Slider> sliders;

    [SerializeField] GameObject PlayerAttackButton;

    [SerializeField] GameObject EnnemisInterface;

    [SerializeField] GameObject PlayerSelectInterface;

    [SerializeField] GameObject ObjectInterface;

    
    List<GameObject> EnnemisMort = new();

    List<GameObject> PlayerMort = new();

    List<Unit> EnemyUnitList = new(); 

    public List<Unit> enemyUnitList
    {
        get { return EnemyUnitList; }
        set { EnemyUnitList = value; }
    }

    List<Unit> PlayerUnitList = new();

    public List<Unit> playerUnitList
    {
        get { return PlayerUnitList; }
        set { PlayerUnitList = value; }
    }

    Competence competence {get; set;}

    public Competence Competence
    {
        get { return competence; }
        set { competence = value; }
    }

    ItemData item;

    public ItemData Item
    {
        get { return item; }
        set { item = value; }
    }

    public bool ActionType;
    

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
        for (int i = 0; i <= PlayerPrefab.Count - 1; i++)
        {
            GameObject PlayerGO = Instantiate(PlayerPrefab[i], PlayerStation[i]);
            PlayerUnit = PlayerGO.GetComponent<Unit>();
            unitsList.Add(PlayerUnit, new());
            PlayerUnit.BarreProg = sliders[i];
            PlayerUnit.BarreProg.maxValue = valMax;
            unitsList[PlayerUnit].Add(PlayerGO);
            
            PlayerUnitList.Add(PlayerUnit);
            
        
        }

        for (int i = 0; i <= EnemyPrefab.Count - 1; i++)
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
                    EnemyUnit = unit;
                    state = BattleStates.EnemyTurn;
                    
                    return;
                }

                PlayerAttackButton.SetActive(true);
                PlayerUnit = unit;
                state = BattleStates.PlayerTurn;
                
                
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


        ObjectInterface.SetActive(true);
        PlayerAttackButton.SetActive(false);

        return;
    } 

    public void OnDefButton()
    {
        if(state != BattleStates.PlayerTurn)
        {
            return;
        }

        PlayerUnit.def = 0.5f;

        state = BattleStates.Neutre;
        PlayerUnit.Progression = 0;
    }

    public void PlayerCompetence(Unit unit)
    {
        DeathCheck();

        if(PlayerUnit.isDef == false)
            PlayerUnit.def = 1;

        PlayerUnit.toursRestant--;

        Debug.Log("Player: " + playerUnit.unitName);
        Debug.Log("Cible: " + unit.unitName);

        if(ActionType)
        {
            ChangeCrystale(competence.IndiceCrystale);
            foreach (Effect effect in competence.effect)
            {
                effect.Apply(playerUnit, unit);
            }
            
        }
        else
        {
            ChangeCrystale(competence.IndiceCrystale);
            foreach (Effect effect in item.effect)
            {
                effect.Apply(playerUnit, unit);
            }
        }   

        bool isDead = unit.IsDead;

        Debug.Log( unit.unitName + ": " + unit.currentHP);
        state = BattleStates.Neutre;
        PlayerUnit.Progression = 0;

        if(unit.Enemy)
        {
            if(isDead)
            {
                EnnemisMort.Add(unitsList[unit][0]);
                Destroy(unitsList[unit][0]);
                Destroy(unitsList[unit][1]);
                
                unitsList.Remove(unit);

                if(EnnemisMort.Count == EnemyPrefab.Count)
                    state = BattleStates.Win;
            }

            EnnemisInterface.SetActive(false);
        }

        else
            PlayerSelectInterface.SetActive(false);
        
        DeathCheck();
        
        return;
    }

    void EndBattle()
    {
        if(state == BattleStates.Win)
            Debug.Log("Gagné !");
        
        else
            Debug.Log("Perdu !");
    }

    void DeathCheck()
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
    }

    void ChangeCrystale(List<int> IndiceCrystale)
    {
        Dictionary<int, RawImage> Cells = Gamecrystal.cells;

        for (int i = 0; i < IndiceCrystale.Count; i++)
        {
            if(Cells.ContainsKey(IndiceCrystale[i]))
                Cells[IndiceCrystale[i]].color = Color.blue;
                
        }
    }

    void BattleTurnPass()
    {

    }

    void CharacterTurnPass(Unit unit)
    {
        unit.ResolveStatus();
    }
}

