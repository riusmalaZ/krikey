using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStates {Start, PlayerTurn, EnemyTurn, Lose, Win, Neutre, Crystal}

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
    
    public List<Unit> units1;
    

    [SerializeField] GameObject PlayerAttackButton;

    [SerializeField] GameObject EnnemisInterface;

    [SerializeField] GameObject PlayerSelectInterface;

    [SerializeField] GameObject ObjectInterface;

    public GameObject CrystalInterface;

    
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

    public Gamecrystal gamecrystal;
    
    public GoldAUgment goldAUgment;
    float valMax;

    public GameObject ecranVictoire;

    public GameObject ecranDefaite;

    public GameObject finCombat;

    public TextMeshProUGUI goldtot;

    public TextMeshProUGUI goldGa;

    public List<EnemyEnnemy> enemyEnnemies;


    // Start is called before the first frame update
    
    void Awake()
    {
        state = BattleStates.Start;
        valMax = 100;
        PlayerAttackButton.SetActive(false);
        EnnemisInterface.SetActive(false);
        PlayerSelectInterface.SetActive(false);

        EnemyPrefab = enemyEnnemies[Random.Range(0, enemyEnnemies.Count)].Enemy;

        ecranVictoire.SetActive(false);
        ecranDefaite.SetActive(false);
        finCombat.SetActive(false);
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

            case BattleStates.Crystal:
                
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
            PlayerUnit.InitializeStat();
            unitsList[PlayerUnit].Add(PlayerGO);
            units1.Add(PlayerUnit);
            PlayerUnitList.Add(PlayerUnit);
            
        
        }

        for (int i = 0; i <= EnemyPrefab.Count - 1; i++)
        {
            GameObject EnemyGO = Instantiate(EnemyPrefab[i], EnemyStation[i]);
            EnemyUnit = EnemyGO.GetComponent<Unit>();
            unitsList.Add(EnemyUnit, new());
            EnemyUnit.InitializeStat();
            units1.Add(EnemyUnit);
            unitsList[EnemyUnit].Add(EnemyGO);
            EnemyUnitList.Add(EnemyUnit);
        }

        goldAUgment.gold.goldGagne = 0;
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
        GameObject gameObject = unitsList[EnemyUnit][0];

        IAEnemy iAEnemy = gameObject.GetComponent<IAEnemy>();

        iAEnemy.battleSysteme = this;

        Competence competence = null;

        List<Unit> units = new();

        List<Unit> unitDeath = new();

        

            
        int nb_Player = PlayerUnitList.Count;

        competence = iAEnemy.GetCompetence(EnemyUnit.competences);

        units = iAEnemy.GetUnits(competence, EnemyUnit.competences);

        Animator enemyAnimator = unitsList[EnemyUnit][0].GetComponentInChildren<Animator>();

        Animator PlayerAnimator = unitsList[units[0]][0].GetComponentInChildren<Animator>();

        if(enemyAnimator != null)
            enemyAnimator.SetTrigger("Attack");

        foreach (Unit unit in units)
        {
            foreach (Effect effect in competence.effect)
            {
                effect.Apply(EnemyUnit, unit);
                if(unit.IsDead)
                    unitDeath.Add(unit);
            }   
        }

        if(PlayerAnimator != null)
            PlayerAnimator.SetTrigger("Hit");

        gamecrystal.ChangeCrystale(competence.IndiceCrystale, true);

        competence.actualCooldown = competence.Cooldown;


        if(unitDeath.Count != 0)
        {
            foreach (Unit unitd in unitDeath)
            {
                PlayerAnimator = unitsList[unitd][0].GetComponentInChildren<Animator>();

                PlayerAnimator.SetTrigger("Death");

                PlayerMort.Add(unitsList[unitd][0]);

                Destroy(unitsList[unitd][0]);
            
                unitsList.Remove(unitd);
            }
            

            if(PlayerMort.Count == nb_Player)
                state = BattleStates.Lose;
                chrono = 0;
                return;
        }

        EnemyUnit.Progression = 0;
        if(gamecrystal.WinCheck() && gamecrystal.CompteTours <= 0)
        {
            gamecrystal.CrystalClone();
            CrystalInterface.SetActive(true);
            return;
        }
        else if(gamecrystal.CompteTours <= 0)
        {
            gamecrystal.AutoDestruction();
            BattleTurnPass(units1);
            CharacterTurnPass(EnemyUnit);
            return;
        }
        else
            BattleTurnPass(units1);
            CharacterTurnPass(EnemyUnit);

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

        PlayerAttackButton.SetActive(false);
        PlayerUnit.def = 0.5f;

        state = BattleStates.Neutre;
        PlayerUnit.Progression = 0;
    }

    public void PlayerCompetence(Unit unit)
    {
        Animator unitAnimator = unitsList[unit][0].GetComponentInChildren<Animator>();

        Animator PlayerAnimator = unitsList[PlayerUnit][0].GetComponentInChildren<Animator>();
        
        DeathCheck();

        if(PlayerUnit.isDef == false)
            PlayerUnit.def = 1;

        PlayerUnit.toursRestant--;

        Debug.Log("Player: " + playerUnit.unitName);
        Debug.Log("Cible: " + unit.unitName);

        List<Unit> units = new();

        if(unit.Enemy)
            units = enemyUnitList;
        else
            units = playerUnitList;

        if(ActionType)
        {
            gamecrystal.ChangeCrystale(competence.IndiceCrystale, false);
            

            if(competence.MultiTarget)
            {
                foreach(Unit unit1 in units)
                {
                    foreach (Effect effect in competence.effect)
                    {
                        effect.Apply(playerUnit, unit1);
                    }
                }
            }

            else{
                foreach (Effect effect in competence.effect)
                {
                    effect.Apply(playerUnit, unit);
                }
            }

            if(unitAnimator != null)
                PlayerAnimator.SetTrigger("Attack");

            if(unitAnimator != null)
                unitAnimator.SetTrigger("Hit");
            
        }
        else
        {
            foreach (Effect effect in item.effect)
            {
                effect.Apply(playerUnit, unit);
            }
            goldAUgment.ObjectUse();
        }   

        competence.actualCooldown = competence.Cooldown;

        bool isDead = unit.IsDead;

        Debug.Log( unit.unitName + ": " + unit.currentHP);
        

        if(unit.Enemy)
        {
            if(isDead)
            {
                if(unitAnimator != null)
                {
                    unitAnimator.SetTrigger("Death");
                }
                
                EnnemisMort.Add(unitsList[unit][0]);
                Destroy(unitsList[unit][0]);
                Destroy(unitsList[unit][1]);
                goldAUgment.MobKillGold();
                unitsList.Remove(unit);

                if(EnnemisMort.Count == EnemyPrefab.Count)
                    state = BattleStates.Win;
            }

            EnnemisInterface.SetActive(false);
        }

        else
            PlayerSelectInterface.SetActive(false);

        if(gamecrystal.WinCheck())
        {
            PlayerUnit.Progression = 0;
            DeathCheck();
            gamecrystal.CrystalClone();
            CharacterTurnPass(playerUnit);
            CrystalInterface.SetActive(true);
            return;
        }

        DeathCheck();
        //BattleTurnPass(units1);
        CharacterTurnPass(playerUnit);

        PlayerUnit.Progression = 0;

        state = BattleStates.Neutre;
        
        return;
    }

    void EndBattle()
    {
        if(state == BattleStates.Win)
        {
            Debug.Log("Gagné !");
            foreach (Unit item in PlayerUnitList)
            {
                goldAUgment.PersoEnVie();
            }
            goldAUgment.gold.goldTotal += goldAUgment.gold.goldGagne;

            finCombat.SetActive(true);
            ecranVictoire.SetActive(true);
            ecranDefaite.SetActive(false);
            goldtot.text = goldAUgment.gold.goldTotal.ToString();
            goldGa.text = goldAUgment.gold.goldGagne.ToString();
            Time.timeScale = 0;
        }
        
        else
        {
            Debug.Log("Perdu !");
            goldAUgment.gold.goldTotal += goldAUgment.gold.goldGagne;
            finCombat.SetActive(true);
            ecranVictoire.SetActive(false);
            ecranDefaite.SetActive(true);
            goldtot.text = goldAUgment.gold.goldTotal.ToString();
            goldGa.text = goldAUgment.gold.goldGagne.ToString();
            Time.timeScale = 0;
        }
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

    

    public void BattleTurnPass(List<Unit> units)
    {
        if(gamecrystal.CompteTours >= 0)
            gamecrystal.DesactiveCrystalTours();

        gamecrystal.CompteTours--;

        foreach (Unit unit in units)
        {
            foreach (Boost boost in unit.Boosts)
            {
            if(boost.boost.isBattleTurn)
                unit.TurnLessBoost();
            }
            foreach (Status status in unit.statuses)
            {
            if(status.effect.isBattleTurn)
                unit.ResolveStatus();
            }
        }
        
    }

    public void CharacterTurnPass(Unit unit)
    {
        foreach (Boost boost in unit.Boosts)
        {
            if(!boost.boost.isBattleTurn)
                unit.TurnLessBoost();
        }
        foreach (Status status in unit.statuses)
        {
            if(!status.effect.isBattleTurn)
                unit.ResolveStatus();
        }

        foreach (Competence item in unit.competences)
        {
            if(item.actualCooldown != 0)
                item.actualCooldown--;
        }
        
    }


}

