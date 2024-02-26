using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{

    public List<GameObject> PlayerPrefab;
    public List<GameObject> EnemyPrefab;

    public int indiceEnemy;

    public int indicePlayer;

    public List<Transform> PlayerBattleStation;
    public List<Transform> EnemyBattleStation;

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
        indiceEnemy = 0; 
        indicePlayer = 0;

        foreach(GameObject P in PlayerPrefab)
        {
            GameObject PlayerGO = Instantiate(P, PlayerBattleStation[indicePlayer]);
            PlayerUnit = PlayerGO.GetComponent<Unit>();
            indicePlayer++;
        }
        
        foreach(GameObject E in PlayerPrefab)
        {
            GameObject EnemyGO = Instantiate(E, EnemyBattleStation[indiceEnemy]);
            EnemyUnit = EnemyGO.GetComponent<Unit>();
            indiceEnemy++;
        }

        indiceEnemy = 0; 
        indicePlayer = 0;

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        PlayerUnit = PlayerPrefab[indicePlayer].GetComponent<Unit>();

        bool isDead = EnemyUnit.TakeDamge(PlayerUnit.damage);
        Debug.Log(EnemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if (indicePlayer == PlayerPrefab.Count - 1 )
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else if(indicePlayer != PlayerPrefab.Count - 1)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
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

        EnemyUnit = EnemyPrefab[indicePlayer].GetComponent<Unit>();

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
