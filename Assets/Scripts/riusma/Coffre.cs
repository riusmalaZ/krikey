using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Coffre : MonoBehaviour
{
    Outline outline;
    public List<string> listItemsName;
    [SerializeField] GameObject prefabButtonChest;

    void Start()
    {
        outline = GetComponent<Outline>();
        foreach (ItemData itemData in GameObject.Find("ItemManager").GetComponent<ItemManager>().ListItems) listItemsName.Add(itemData.Name);
    }

    void OnMouseOver()
    {
        outline.enabled = true;
        if (Input.GetMouseButton(0)) openChest();

    }

    void OnMouseExit()
    {
        outline.enabled = false;
    }


    void openChest()
    {
        GetComponent<BoxCollider>().enabled = false;
        List<string> listItemsName2 = new List<string>();
        foreach (string name in listItemsName) listItemsName2.Add(name);
        int x = 0;
        GameObject objInst;
        for (int i = 0; i < 3; i++)
        {
            int n = UnityEngine.Random.Range(0, 6 - i);
            objInst = Instantiate(prefabButtonChest);

            Button button = objInst.GetComponent<Button>();
            Button.AddListener(objInst.GetComponent<ButtonScript>().Pressed(GameObject.Find("ItemManager").GetComponent<ItemManager>().ListItems[n]));
            objInst.GetComponentInChildren<TextMeshProUGUI>().text = listItemsName2[n];

            if (i == 1) x = 250;
            if (i == 2) x = -250;
            objInst.GetComponent<RectTransform>().position = new Vector3(0 + x, 0, 0);
            objInst.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>(), false);
            listItemsName2.RemoveAt(n);

        }
    }
}
