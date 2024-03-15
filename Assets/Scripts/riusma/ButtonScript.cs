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
            //load ta scene de combat
            ReloadScene();
        }
        else if (lvlSelected == "coffre")
        {
            GameObject.Find("RaycastManager").GetComponent<RaycastManager>().objCoffre.SetActive(true);
            //remplir les cartes
        }
        else if (lvlSelected == "etoile_event")
        {
            GameObject.Find("RaycastManager").GetComponent<RaycastManager>().objScenar[0].SetActive(true);
        }
        else if (lvlSelected == "etoile_event1")
        {
            GameObject.Find("RaycastManager").GetComponent<RaycastManager>().objScenar[1].SetActive(true);
        }
        else if (lvlSelected == "etoile_event2")
        {
            GameObject.Find("RaycastManager").GetComponent<RaycastManager>().objScenar[2].SetActive(true);
        }
        else if (lvlSelected == "feu_de_camp")
        {
            //rajoute des hp omg
            ReloadScene();
        }
        else 
        {
            print("azy ya r");
            ReloadScene();
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
