using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventoryData inventoryData;
    [SerializeField] List<GameObject> slots;

    void Start()
    {
        /*for (int i = 0; i < 6; i++)
        {
            slots.Add(GameObject.Find("Slot" + (i+1).ToString()));
        }*/

        if (inventoryData.Inventaire.Count != 0)
        {
            for (int i = 0; i <  inventoryData.Inventaire.Count; i++)
            {
                slots[i].GetComponentInChildren<TextMeshProUGUI>().text = inventoryData.Inventaire[i];
                slots[i].GetComponent<Button>().enabled = true;
            }
        }
    }

    public void AddToInventory(ItemData itemData)
    {
        if (inventoryData.Inventaire.Count < 6)
        {
            inventoryData.Inventaire.Add(itemData.Name);
            int nObj = inventoryData.Inventaire.Count - 1;
            slots[nObj].GetComponentInChildren<TextMeshProUGUI>().text = itemData.Name;
            slots[nObj].GetComponent<Button>().enabled = true;
        }

        else print("t gros mdr");
    }

    public void RemoveToInventory(string nameSlot)
    {
        int i = 0;
        while ("Slot" + i.ToString() != nameSlot) i++;
        print(slots[i - 1].GetComponentInChildren<TextMeshProUGUI>().text + " used.");
        inventoryData.Inventaire.Remove(slots[i - 1].GetComponentInChildren<TextMeshProUGUI>().text);
        slots[i - 1].GetComponentInChildren<TextMeshProUGUI>().text = "";
        slots[i - 1].GetComponent<Button>().enabled = false;
    }
}
