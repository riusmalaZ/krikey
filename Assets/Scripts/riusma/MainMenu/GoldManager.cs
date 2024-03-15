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
    // Start is called before the first frame update
    void Start()
    {
        UpdateGold(0);
        float golds = inv.Gold;
        if (inv.Gold > 100) goldBar.fillAmount = 1;
        else goldBar.fillAmount = golds / 100;
    }

    // Update is called once per frame
    void UpdateGold(int amount)
    {
        inv.Gold += amount;
        textGold.text = inv.Gold.ToString() + "G / 1OOG";        
    }
}
