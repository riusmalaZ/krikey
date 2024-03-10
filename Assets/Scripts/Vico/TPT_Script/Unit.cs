using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [Header("Nom de l'unité")]
    [Space]
    public string unitName;

    [Header("Statistique de l'unité")]
    [Space]
    public int unitLevel;

    public int damage;

    public int maxHP;

    public int attackSpeed;
    
    public float def;

    public int toursRestant;

    [Header("Ennemis ou Personnage")]
    [Space]
    public bool Enemy;

    [Header("Compétence :")]
    [Space]
    public Competence[] competences;

    [HideInInspector]
    public bool IsDead => currentHP <= 0;

    [HideInInspector]
    public float Progression;

    public Slider BarreProg;
    
    [HideInInspector]
    public bool isDef => def == 1;

    [HideInInspector]
    public List<Status> statuses = new();

    [HideInInspector]
    public List<Boost> Boosts = new();

    [HideInInspector]
    public bool stun = false;

    [HideInInspector]
    public int currentHP;

    [HideInInspector]
    public int currentAttSpeed;

    [HideInInspector]
    public float currentDef;

    [HideInInspector]
    public float currentDamage;


    public void InitializeStat()
    {
        currentHP = maxHP;
        currentAttSpeed  = attackSpeed;
        currentDamage = damage;
        currentDef = def;
    }

    public bool TakeDamge(int dmg)
    {
        currentHP -= (int)Math.Round(dmg*currentDef);

        return IsDead;
    }

    public void Progress()
    {
        Progression += 1 * (currentAttSpeed * Time.deltaTime); // Incrémente la valeur "progression" de l'unité
        BarreProg.value = Progression;
    }

    public void BeingHeal(int heal)
    {
        if(currentHP + heal >= maxHP)
        {
            currentHP = maxHP;
        }

        else{
            currentHP += heal;
        }
    }

    public void ApplyStatus(StatusEffect effect, int NbTours, Unit Attaquant)
    {
        if(!effect.Stackable)
        {
            foreach (Status status in statuses)
            {
                if(status.effect == effect && effect.CanBeRefresh)
                {
                    if(status.NbTours < effect.duration)
                    {
                        status.Refresh();
                    }

                    return;
                }
            }
        }
        
        statuses.Add(new Status(effect, NbTours,  Attaquant));

    }

    public void RemoveStatus(Status status)
    {
        statuses.Remove(status);
    }

    internal void ResolveStatus()
    {
        foreach (Status status in statuses)
        {
            status.Resolve(this);
        }
    }


    public void RemoveBoost(Boost boost)
    {
        Boosts.Remove(boost);
    }

    public void ApplyBoost(BoostEffect boostEffect, int NbTours, Unit Receveur)
    {
        if(!boostEffect.Stackable)
        {
            foreach (Boost boost in Boosts)
            {
                if(boost.boost == boostEffect && boostEffect.CanBeRefresh)
                {
                    if(boost.NbTours < boostEffect.Duration)
                        boost.Refresh();
                }
            }
        }

        Boosts.Add(new Boost(boostEffect, NbTours, Receveur));
    }

    internal void TurnLessBoost()
    {
        foreach (Boost boost in Boosts)
        {
            boost.TurnLess(this);
        }
    }
}
