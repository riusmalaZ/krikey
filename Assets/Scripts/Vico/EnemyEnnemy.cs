using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PaternEnemy", menuName = "ScriptablesObjects/PaternEnemy")]
public class EnemyEnnemy : ScriptableObject
{
    public List<GameObject> Enemy = new();
}
