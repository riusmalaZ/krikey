using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [HideInInspector] public new string name;
    [HideInInspector] public string lvlSelected;
    public void Equip()
    {
        /*foreach (ItemData itemData in GameObject.Find("ItemManager").GetComponent<ItemManager>().ListItems)
            if (itemData.Name == name) GameObject.Find("Inventory").GetComponent<Inventory>().AddToInventory(itemData);
        GameObject.Find("Coffre").GetComponent<Coffre>().Clear();*/
        
    }
    public void Use()
    {
        //GameObject.Find("Inventory").GetComponent<Inventory>().RemoveToInventory(gameObject.name);
    }
    public void Play()
    {
        GameObject.Find("PathManager").GetComponent<PathManager>().NextPoint();
        
        if (lvlSelected == "combat")
        {
            print("bats toi fumier");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (lvlSelected == "coffre")
        {
            print("la kichta O___o");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else 
        {
            print("azy ya r");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
        }
    }
}
