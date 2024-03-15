using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textGold;
    [SerializeField] InventoryData inv;
    [SerializeField] Image goldBar;
    [SerializeField] GameObject buttonUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        UpdateGold(0);
        float golds = inv.Gold;
        if (golds >= 100) 
        {
            print("we");
            buttonUpgrade.SetActive(true);
            goldBar.fillAmount = 1;
        }
        else
        {
            print("nn");
            goldBar.fillAmount = golds / 100;
        }
    }

    // Update is called once per frame
    void UpdateGold(int amount)
    {
        inv.Gold += amount;
        textGold.text = inv.Gold.ToString() + "G / 1OOG";        
    }

    public void Upgrade()
    {
        inv.Gold -=100;
        textGold.text = inv.Gold.ToString() + "G / 1OOG";
        float golds = inv.Gold;
        if (golds >= 100) goldBar.fillAmount = 1;
        else 
        {
            buttonUpgrade.SetActive(false);
            goldBar.fillAmount = golds / 100;
        }
    }
}
