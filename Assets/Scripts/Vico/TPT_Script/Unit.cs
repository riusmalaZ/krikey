using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    public string unitName;
    public int unitLevel;

    public int damage;

    public int heal;

    public int maxHP;
    public int currentHP;
    public int attackSpeed;

    public float Progression;

    public Slider BarreProg;

    public bool Enemy;

    public bool TakeDamge(int dmg)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
            return true;
        else
            return false;
    }
}
