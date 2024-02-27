using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "ScriptablesObjects/Inventory")]
public class InventoryData : Deletables
{
    public List<Item> Invetaire;
    public override void Delete()
    {
        base.Delete();
    }
}
