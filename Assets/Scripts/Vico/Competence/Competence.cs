using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Competence", menuName = "ScriptablesObjects/Competence")]
public class Competence : ScriptableObject
{
    public bool friendly = false;
    public string nom;
    public int Cooldown;

    public List<Effect> effect;

    public List<int> IndiceCrystale = new();
}
