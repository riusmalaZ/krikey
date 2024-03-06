using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "ScriptablesObjects/Inventory")]
public class InventoryData : Resetable
{
    public ItemData[] Inventaire;
    public override void Reset()
    {
        base.Reset();
        Array.Clear(Inventaire, 0, Inventaire.Length);
    }

    public int Gold;

    public void ObjectUse(ItemData item)
    {
        int indiceItem = Array.IndexOf(Inventaire, item);

        Debug.Log("indice " + indiceItem);

        if (indiceItem != -1)
        {
            // Effacer l'élément à cet indice
            Array.Clear(Inventaire, indiceItem, 1);
        }

        for (int i = indiceItem + 1; i < Inventaire.Length; i++)
        {
            Inventaire[i - 1] = Inventaire[i];
        }

        Inventaire[Inventaire.Length - 1] = null;
    }
}
