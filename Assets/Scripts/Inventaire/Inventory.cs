using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryData inventoryData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddToInventory(ItemData itemData)
    {
        inventoryData.Inventaire.Add(itemData.Description);
    }
}
