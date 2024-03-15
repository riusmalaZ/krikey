using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecrystal : MonoBehaviour
{

    public List<int> PlayerCell = new();

    public List<int> EnemyCell = new();

    public GameObject[] toursCrystal;

    public GameObject[] toursCrystalSauv;

    public Sprite PlayerCrystal;

    public Sprite EnemyCrystal;

    public Sprite NeutralCrystal;

    public int CompteTours = 6; 
    // Start is called before the first frame update
    public static Dictionary<int, Image> cells = new Dictionary<int, Image>();

    public GameObject CrystalToClone;

    public GameObject CloneTarget;

    public RectTransform parentCanvas;

    GameObject clonedObject;

    int[] PourcentageHpBonus = {40, 30, 25, 20, 15, 10, 5};

    int[] PourcentageAttBonus = {30, 22, 18,15, 12, 8, 5};
                                
    int[] ToursBonus = {3, 2, 2, 2, 1, 1, 1};

    public HealCrystal healCrystal;

    public damageBonus damageBonus;

    public ToursBonus toursBonus;

    public BattleSysteme battleSysteme;


    void Start()
    {
        toursCrystalSauv = toursCrystal;
        CrystalToClone = GameObject.FindWithTag("Crystal");
        healCrystal.Pourcentage = PourcentageHpBonus[6];
        damageBonus.Pourcentage = PourcentageAttBonus[6];
        toursBonus.Tours = ToursBonus[6];
        
    }

    public static void RegisterToCrystal(int indice, Image image)
    {
        if(!cells.ContainsKey(indice))
        {
            cells.Add(indice, image);
        }
    }

    public void ChangeCrystale(List<int> IndiceCrystale, bool Enemy)
    {
        Dictionary<int, Image> Cells = Gamecrystal.cells;

        for (int i = 0; i < IndiceCrystale.Count; i++)
        {
            if(Cells.ContainsKey(IndiceCrystale[i]))
            {
                if(Enemy)
                {
                    Cells[IndiceCrystale[i]].sprite = EnemyCrystal;

                    if(!EnemyCell.Contains(IndiceCrystale[i]))
                        EnemyCell.Add(IndiceCrystale[i]);

                    if(PlayerCell.Contains(IndiceCrystale[i]))
                    {
                        PlayerCell.Remove(IndiceCrystale[i]);
                    }
                }

                else
                {
                    Cells[IndiceCrystale[i]].sprite = PlayerCrystal;

                    if(!PlayerCell.Contains(IndiceCrystale[i]))
                        PlayerCell.Add(IndiceCrystale[i]);

                    if(EnemyCell.Contains(IndiceCrystale[i]))
                    {
                        EnemyCell.Remove(IndiceCrystale[i]);
                    }
                }
            }
        }
    }

    public void BrokeCrystale(List<int> IndiceCrystale, bool Enemy)
    {
        Dictionary<int, Image> Cells = Gamecrystal.cells;
        battleSysteme.goldAUgment.NbCrystalBrise();
        foreach (int i  in IndiceCrystale)
        {
            int Indice = i;
            
            if(Enemy)
            {
                Cells[Indice].sprite = NeutralCrystal;
                EnemyCell.Remove(Indice);
            }

            else if(Enemy == false)
            {
                Cells[Indice].sprite = NeutralCrystal;
                PlayerCell.Remove(Indice);
            }
            
        }

        if(Enemy)
            EnemyCell.Clear();
        else
            PlayerCell.Clear();

        CompteTours = 6;
    }

    public void AutoDestruction()
    {
        
        Dictionary<int, Image> Cells = Gamecrystal.cells;

        foreach (int Icrystal in Cells.Keys)
        {
            cells[Icrystal].sprite = NeutralCrystal;
        }

        foreach (GameObject tours in toursCrystal)
        {
            tours.SetActive(true);
        }

        PlayerCell.Clear();
        EnemyCell.Clear();

        CompteTours = 6;
    }

    
    public void CrystalClone()
    {
        clonedObject = Instantiate(CrystalToClone);
       
        clonedObject.transform.parent = CloneTarget.transform;

        // Réinitialiser la position et la rotation par rapport au parent si nécessaire
        clonedObject.transform.position = CloneTarget.transform.position;
        clonedObject.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        CloneTarget.transform.position = new Vector3(336f, 171f, 0f);
        clonedObject.transform.localRotation = Quaternion.identity;
    }

    public void DestroyClone()
    {
        Destroy(clonedObject);
    }

    public void EffectManager()
    {
        healCrystal.Pourcentage = PourcentageHpBonus[CompteTours - 1];
        damageBonus.Pourcentage = PourcentageAttBonus[CompteTours - 1];
        toursBonus.Tours = ToursBonus[CompteTours - 1];
    }

    public bool WinCheck()
    {
        bool isWining = false;

        if(EnemyCell.Count == 0 && PlayerCell.Count == 0)
            return isWining;

        if(EnemyCell.Count <= PlayerCell.Count)
            isWining = true;

        return isWining;
    }

    public void ApplyHeal()
    {
        for (int i = 0; i < battleSysteme.playerUnitList.Count; i++)
        {
            healCrystal.Apply(battleSysteme.playerUnitList[i],battleSysteme.playerUnitList[i]);
        }

        battleSysteme.state = BattleStates.Neutre;
        battleSysteme.CrystalInterface.SetActive(false);
        Destroy(clonedObject);

        if(CompteTours > 0)
        {
            BrokeCrystale(PlayerCell,false);
            battleSysteme.BattleTurnPass(battleSysteme.units1);
            battleSysteme.CharacterTurnPass(battleSysteme.playerUnit);
        }
        else if(CompteTours <= 0)
            AutoDestruction();
    }

    public void ApplyDamage()
    {
        for (int i = 0; i < battleSysteme.enemyUnitList.Count; i++)
        {
            damageBonus.Apply(battleSysteme.enemyUnitList[i],battleSysteme.enemyUnitList[i]);
        }

        battleSysteme.state = BattleStates.Neutre;
        battleSysteme.CrystalInterface.SetActive(false);
        Destroy(clonedObject);

        if(CompteTours > 0)
        {
            BrokeCrystale(PlayerCell,false);
            battleSysteme.BattleTurnPass(battleSysteme.units1);
            battleSysteme.CharacterTurnPass(battleSysteme.playerUnit);
        }
        else if(CompteTours <= 0)
            AutoDestruction();
    }

    public void ApplyTours()
    {
        for (int i = 0; i < battleSysteme.playerUnitList.Count; i++)
        {
            toursBonus.Apply(battleSysteme.playerUnitList[i],battleSysteme.playerUnitList[i]);
        }

        battleSysteme.state = BattleStates.Neutre;
        battleSysteme.CrystalInterface.SetActive(false);
        Destroy(clonedObject);

        if(CompteTours > 0)
        {
            BrokeCrystale(PlayerCell,false);
            battleSysteme.BattleTurnPass(battleSysteme.units1);
            battleSysteme.CharacterTurnPass(battleSysteme.playerUnit);
        }
        else if(CompteTours <= 0)
            AutoDestruction();
    }

    public void PassTours()
    {
        battleSysteme.state = BattleStates.Neutre;
        battleSysteme.CrystalInterface.SetActive(false);
        Destroy(clonedObject);

        if(CompteTours > 0)
        {
            battleSysteme.BattleTurnPass(battleSysteme.units1);
            battleSysteme.CharacterTurnPass(battleSysteme.playerUnit);
        }
            
        else if(CompteTours <= 0)
            AutoDestruction();
    }

    public void DesactiveCrystalTours()
    {
        Debug.Log(CompteTours);
        toursCrystal[CompteTours].SetActive(false);
    }
    
    public void ActiveCrystalTours()
    {
        foreach (GameObject item in toursCrystal)
        {
            item.SetActive(true);
        }
    }
}
