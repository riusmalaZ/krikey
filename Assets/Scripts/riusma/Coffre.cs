using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coffre : MonoBehaviour
{
    Outline outline;
    [SerializeField] List<GameObject> buttonsItems;

    void Start()
    {
        outline = GetComponent<Outline>();
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
        List<GameObject> buttonsItems2 = buttonsItems;

        int x = 0;
        GameObject objInst;
        for (int i = 0; i < 3; i++)
        {
            int n = UnityEngine.Random.Range(0, 6 - i);
            objInst = Instantiate(buttonsItems2[n]);
            if (i == 1) x = 500;
            if (i == 2) x = -500;
            objInst.GetComponent<RectTransform>().position = new Vector3(0 + x, 0, 0);
            objInst.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>(), false);
            buttonsItems2.RemoveAt(n);

        }
    }
}
