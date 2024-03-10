using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecrystal : MonoBehaviour
{

    public List<int> PlayerCell = new();

    public List<int> EnemyCell = new();

    public RawImage PlayerCrystal;

    public RawImage EnemyCrystal;

    public RawImage NeutralCrystal;

    public int CompteTours = 7; 
    // Start is called before the first frame update
    public static Dictionary<int, RawImage> cells = new Dictionary<int, RawImage>();
    void Awake()
    {
        
    }

    public static void RegisterToCrystal(int indice, RawImage image)
    {
        if(!cells.ContainsKey(indice))
        {
            cells.Add(indice, image);
        }
    }

    public void ChangeCrystale(List<int> IndiceCrystale, bool Enemy)
    {
        Dictionary<int, RawImage> Cells = Gamecrystal.cells;

        for (int i = 0; i < IndiceCrystale.Count; i++)
        {
            if(Cells.ContainsKey(IndiceCrystale[i]))
            {
                if(Enemy)
                {
                    Cells[IndiceCrystale[i]] = EnemyCrystal;
                    EnemyCell.Add(IndiceCrystale[i]);

                    if(PlayerCell.Contains(IndiceCrystale[i]))
                    {
                        PlayerCell.Remove(IndiceCrystale[i]);
                    }
                }

                else
                {
                    Cells[IndiceCrystale[i]] = PlayerCrystal;
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
        Dictionary<int, RawImage> Cells = Gamecrystal.cells;

        for (int i = 0; i < IndiceCrystale.Count; i++)
        {
            if(Cells.ContainsKey(IndiceCrystale[i]))
            {
                if(Enemy)
                {
                    Cells[IndiceCrystale[i]] = NeutralCrystal;
                    EnemyCell.Remove(IndiceCrystale[i]);
                }

                else
                {
                    Cells[IndiceCrystale[i]] = NeutralCrystal;
                    PlayerCell.Remove(IndiceCrystale[i]);
                }
            }
        }
    }

    public void AutoDestruction()
    {
        Dictionary<int, RawImage> Cells = Gamecrystal.cells;

        foreach (int Icrystal in Cells.Keys)
        {
            cells[Icrystal] = NeutralCrystal;
        }
    }

    
}
