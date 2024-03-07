using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textGold;
    [SerializeField] InventoryData inv;
    // Start is called before the first frame update
    void Start()
    {
        UpdateGold(0);
    }

    // Update is called once per frame
    void UpdateGold(int amount)
    {
        inv.Gold += amount;
        textGold.text = inv.Gold.ToString() + "G";
    }
}
