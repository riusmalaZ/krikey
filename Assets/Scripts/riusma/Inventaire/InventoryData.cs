using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "ScriptablesObjects/Inventory")]
public class InventoryData : Resetable
{
    public List<string> Inventaire;
    public override void Reset()
    {
        base.Reset();
        Inventaire = new List<string>();
    }
}
