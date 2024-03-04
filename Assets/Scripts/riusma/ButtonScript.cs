using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [HideInInspector] public string name;

    public void Equip()
    {
        foreach (ItemData itemData in GameObject.Find("ItemManager").GetComponent<ItemManager>().ListItems)
            if (itemData.Name == name) GameObject.Find("Inventory").GetComponent<Inventory>().AddToInventory(itemData);
        GameObject.Find("Coffre").GetComponent<Coffre>().Clear();
        
    }
    public void Use()
    {
        GameObject.Find("Inventory").GetComponent<Inventory>().RemoveToInventory(gameObject.name);
    }
    public void Play()
    {
        GameObject.Find("PathManager").GetComponent<PathManager>().NextPoint();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
