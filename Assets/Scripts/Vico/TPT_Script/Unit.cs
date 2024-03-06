using System;
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

    public bool IsDead => currentHP <= 0;

    public float Progression;

    public Slider BarreProg;

    public int toursRestant;

    public bool Enemy;

    public float def;

    public bool isDef => def == 1;

    public Competence[] competences;

    public bool TakeDamge(int dmg)
    {
        currentHP -= (int)Math.Round(dmg*def);

        return IsDead;
    }

    public void Progress()
    {
        Progression += 1 * (attackSpeed * Time.deltaTime); // Incrémente la valeur "progression" de l'unité
        BarreProg.value = Progression;
    }

    public void BeingHeal(int heal)
    {
        if(currentHP + heal == maxHP)
        {
            currentHP = maxHP;
        }

        else{
            currentHP += heal;
        }
    }

    
}
