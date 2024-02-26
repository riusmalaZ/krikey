using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public bool TakeDamge(int dmg)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
            return true;
        else
            return false;
    }
}
