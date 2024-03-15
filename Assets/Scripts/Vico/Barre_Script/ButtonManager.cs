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

    [SerializeField]
    List<Slider> slidervie;
    
    void Start()
    {
        AssociationButton();
    }

    void Update()
    {
        List<Unit> playerUnits = battleSysteme.playerUnitList;

        for (int i = 0; i < slidervie.Count; i++)
        {
            slidervie[i].value = playerUnits[i].currentHP;
        }
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
                texts.text = enemyUnits[currentIndex].unitName + " " + enemyUnits[currentIndex].currentHP.ToString();
                
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
                Unit unit2 = playerUnits[currentIndex];
                texts.text = unit.unitName;

                slidervie[currentIndex].maxValue = unit.maxHP;

                button.onClick.AddListener(() => BoutonPlayerClique(unit2));
                Debug.Log(unit.unitName);
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

        Texture sprite = null;

        Debug.Log("Nom du joueur " + player.unitName);

        for (int i = 0; i <= competenceList.Length - 1; i++)
        {
            int y = i;
            if(competenceList[y] != null)
            {
                
                if(i < competenceList.Length)
                {
                    if(competenceList[y].actualCooldown != 0)
                    {
                        ButtonComp[y].SetActive(true);
                        button = ButtonComp[y].GetComponent<Button>();
                        button.enabled = false;
                        texts = ButtonComp[y].GetComponentInChildren<TextMeshProUGUI>();
                        sprite = competenceList[y].Crystale;
                        ButtonComp[y].GetComponentInChildren<RawImage>().texture = sprite;
                        texts.text = competenceList[y].nom;
                        texts.color = Color.gray;
                        Debug.Log(i);
                        competence = competenceList[y];
                        var localCompetence = competenceList[y];
                        @object = ButtonComp[y];
                        button.onClick.AddListener(() => ButtonCompClique(localCompetence));
                    }
                    else
                    {
                        ButtonComp[y].SetActive(true);
                        button = ButtonComp[y].GetComponent<Button>();
                        button.enabled = true;
                        texts = ButtonComp[y].GetComponentInChildren<TextMeshProUGUI>();
                        sprite = competenceList[i].Crystale;
                        ButtonComp[y].GetComponentInChildren<RawImage>().texture = sprite;
                        texts.text = competenceList[y].nom;
                        texts.color = Color.white;
                        Debug.Log(i);
                        competence = competenceList[y];
                        var localCompetence = competenceList[y];
                        @object = ButtonComp[y];
                        button.onClick.AddListener(() => ButtonCompClique(localCompetence));
                    }
                }
            }
            else
                ButtonComp[y].SetActive(false);
                Debug.Log("Il est null");
        }
    }

    void ButtonCompClique(Competence competence)
    {
        battleSysteme.Competence = competence;
        battleSysteme.ActionType = true;
        
        Debug.Log(competence.nom);

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


