using System;
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

    [SerializeField]
    GameObject EnemyPanel;

    [SerializeField]
    GameObject PlayerPanel;

    [SerializeField]
    GameObject CompPanel;

    [SerializeField]
    GameObject ObjectPanel;

    [SerializeField]
    InventoryData inventory;
    
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

        Unit unit = null;

        for (int i = 0; i <= ButtonEnnemis.Length - 1; i++)
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
        unit = null;

        for (int i = 0; i <= ButtonPlayer.Length - 1; i++)
        {
            if(i < playerUnits.Count)
            {
                int currentIndex = i;
                button = ButtonPlayer[currentIndex].GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                texts = ButtonPlayer[currentIndex].GetComponentInChildren<TextMeshProUGUI>();
                unit = playerUnits[currentIndex];
                texts.text = unit.unitName;

                
                Debug.Log("y" + i);
                button.onClick.AddListener(() => BoutonPlayerClique(unit));
                
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
            battleSysteme.PlayerCompetence(ennemi);

    }

    void BoutonPlayerClique(Unit player)
    {
        Debug.Log(player.unitName);
        battleSysteme.PlayerCompetence(player);
    }

    public void GetPlayerCompetence()
    {
        Unit player = battleSysteme.playerUnit;

        Competence[] competenceList = player.competences;

        Button button = null;

        TextMeshProUGUI texts;

        Competence competence = null;

        GameObject @object =null;

        Debug.Log("Nom du joueur " + player.unitName);

        for (int i = 0; i <= competenceList.Length - 1; i++)
        {
            if(competenceList[i] != null)
            {
                
                if(i < competenceList.Length)
                {
                    ButtonComp[i].SetActive(true);
                    button = ButtonComp[i].GetComponent<Button>();
                    texts = ButtonComp[i].GetComponentInChildren<TextMeshProUGUI>();
                    texts.text = competenceList[i].nom;
                    Debug.Log(i);
                    competence = competenceList[i];
                    @object = ButtonComp[i];
                    button.onClick.AddListener(() => ButtonCompClique(competence));
                }
            }
            else
                ButtonComp[i].SetActive(false);
                Debug.Log("Il est null");
        }
    }

    void ButtonCompClique(Competence competence)
    {
        battleSysteme.Competence = competence;
        battleSysteme.ActionType = true;
        
        Debug.Log(competence.friendly == false);

        if(competence.friendly == false)
        {
            CompPanel.SetActive(false);
            EnemyPanel.SetActive(true);
            return;
        }
        else
            CompPanel.SetActive(false);
            PlayerPanel.SetActive(true);
    }

    public void inventoryButtons()
    {
        ItemData[] objectInv = inventory.Inventaire;

        ItemData item =null;

        Button button = null;

        TextMeshProUGUI texts;

        for (int i = 0; i < objectInv.Length; i++)
        {
            if(objectInv[i] != null)
            {
                
                ButtonObject[i].SetActive(true);
                button = ButtonObject[i].GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                texts = ButtonObject[i].GetComponentInChildren<TextMeshProUGUI>();

                texts.text = objectInv[i].Name;
                item = objectInv[i];

                button.onClick.AddListener(() => OnButtonObjectClick(item));
            }

            else
                ButtonObject[i].SetActive(false);
        }
    }

    public void OnButtonObjectClick(ItemData item)
    {
        battleSysteme.Item = item;
        battleSysteme.ActionType = false;

        if(item.friendly == false)
        {
            ObjectPanel.SetActive(false);
            EnemyPanel.SetActive(true);
            inventory.ObjectUse(item);

            return;
        }
        else
            ObjectPanel.SetActive(false);
            PlayerPanel.SetActive(true);
            inventory.ObjectUse(item);
    }
}


