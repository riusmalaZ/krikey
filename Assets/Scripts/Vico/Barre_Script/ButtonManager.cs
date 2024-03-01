using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager: MonoBehaviour
{
    [SerializeField]
    BattleSysteme battleSysteme;
    
    [SerializeField]
    GameObject[] ButtonPlayer;

    [SerializeField]
    GameObject[] ButtonEnnemis;

    [SerializeField]
    GameObject[] ButtonObject;

    [SerializeField]
    GameObject[] ButtonComp;

    
    void Start()
    {
        
        
        AssociationButton();
    }

    void AssociationButton()
    {
        List<Unit> playerUnits = battleSysteme.playerUnitList;

        List<Unit> enemyUnits = battleSysteme.enemyUnitList;

        Button button = null;

        TextMeshProUGUI texts;

        for (int i = 0; i < ButtonEnnemis.Length - 1; i++)
        {
            if (i < enemyUnits.Count) // Vérifie si l'index est inférieur à la taille de enemyUnits
            {
                int currentIndex = i;
                button = ButtonEnnemis[currentIndex].GetComponent<Button>();
                texts = ButtonEnnemis[currentIndex].GetComponentInChildren<TextMeshProUGUI>();
                texts.text = enemyUnits[currentIndex].unitName;
                Debug.Log(currentIndex);
                button.onClick.AddListener(() => BoutonEnnemiClique(enemyUnits[currentIndex]));
            }
            else
            {
                ButtonEnnemis[i].SetActive(false); // Désactive le bouton s'il n'y a pas d'unité correspondante
            }
        }   

        button = null;

        for (int i = 0; i <= ButtonPlayer.Length - 1; i++)
        {
            if(i < playerUnits.Count)
            {
                button = ButtonPlayer[i].GetComponent<Button>();
                texts = ButtonPlayer[i].GetComponentInChildren<TextMeshProUGUI>();
                texts.text = playerUnits[i].unitName;
                Debug.Log(i);
                button.onClick.AddListener(() => BoutonEnnemiClique(playerUnits[i-1]));
            }
            
            else
            {
                ButtonPlayer[i].SetActive(false); // Désactive le bouton s'il n'y a pas d'unité correspondante
            }
        }
        
    }
    
    void BoutonEnnemiClique(Unit ennemi)
    {
        if(battleSysteme.state == BattleStates.PlayerTurn)
            battleSysteme.PlayerAttack(ennemi);
    }

    void BoutonPlayerClique(Unit player)
    {
        battleSysteme.PlayerAttack(player);
    }

    
    
}
