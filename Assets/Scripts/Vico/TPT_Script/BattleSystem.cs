using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public TextMeshProUGUI texts;

    public List<GameObject> PlayerPrefab;
    public List<GameObject> EnemyPrefab;

    int indiceEnemy;

    int indicePlayer;

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
        
        foreach(GameObject E in EnemyPrefab)
        {
            GameObject EnemyGO = Instantiate(E, EnemyBattleStation[indiceEnemy]);
            EnemyUnit = EnemyGO.GetComponent<Unit>();
            indiceEnemy++;
        }

        indiceEnemy = 0; 
        indicePlayer = 0;

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;

        PlayerUnit = PlayerPrefab[0].GetComponent<Unit>();

        texts.text = PlayerUnit.unitName;

        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {

        PlayerUnit = PlayerPrefab[indicePlayer].GetComponent<Unit>();

        texts.text = PlayerUnit.unitName;

        bool isDead = false;
        Debug.Log( EnemyUnit.unitName + ": " +EnemyUnit.currentHP);

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if (indicePlayer == PlayerPrefab.Count - 1 )
        {
            state = BattleState.ENEMYTURN;
            indicePlayer = 0;
            StartCoroutine(EnemyTurn());
        }
        else if(indicePlayer != PlayerPrefab.Count - 1)
        {
            state = BattleState.PLAYERTURN;
            indicePlayer++;

            PlayerUnit = PlayerPrefab[indicePlayer].GetComponent<Unit>();

            texts.text = PlayerUnit.unitName;

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

        Debug.Log("Enemy Turn");

        EnemyUnit = EnemyPrefab[indiceEnemy].GetComponent<Unit>();

        texts.text = EnemyUnit.unitName;

        bool isDead = false;

        Debug.Log(PlayerUnit.unitName + ": " + PlayerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else if (indiceEnemy == EnemyPrefab.Count - 1 )
        {
            state = BattleState.PLAYERTURN;
            indiceEnemy = 0;

            PlayerUnit = PlayerPrefab[0].GetComponent<Unit>();

            texts.text = PlayerUnit.unitName;

            PlayerTurn();
        }
        else if(indiceEnemy != EnemyPrefab.Count - 1)
        {
            state = BattleState.ENEMYTURN;
            indiceEnemy++;
            
            StartCoroutine(EnemyTurn());
        }
    }

    void PlayerTurn()
    {
        Debug.Log("Player Turn");

    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            Debug.Log("ah bah nan");
            return;
        }

        Debug.Log("Appuie");
        StartCoroutine(PlayerAttack());
    } 
}
