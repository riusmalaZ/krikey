using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{

    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public Transform PlayerBattleStation;
    public Transform EnemyBattleStation;

    public BattleState state; 

    Unit PlayerUnit;
    Unit EnemyUnit;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }


    IEnumerator SetupBattle()
    {
        GameObject PlayerGO = Instantiate(PlayerPrefab, PlayerBattleStation);
        PlayerUnit = PlayerGO.GetComponent<Unit>();

        GameObject EnemyGO = Instantiate(EnemyPrefab, EnemyBattleStation);
        EnemyUnit = EnemyGO.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = EnemyUnit.TakeDamge(PlayerUnit.damage);
        Debug.Log(EnemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            Debug.Log("Gagné");
        }
        else if(state == BattleState.LOST)
        {
            Debug.Log("Perdu");
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        bool isDead = PlayerUnit.TakeDamge(EnemyUnit.damage);
        Debug.Log(PlayerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void PlayerTurn()
    {
        Debug.Log("Player Turn");
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    } 
}
