using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void Pressed(ItemData itemData)
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().AddToInventory(itemData);
    }
}
