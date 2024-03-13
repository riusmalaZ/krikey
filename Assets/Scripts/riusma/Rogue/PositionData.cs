using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PositionData", menuName = "ScriptablesObjects/PositionData")]
public class PositionData : Resetable
{
    public Vector2Int position;
    public int nGrille1;
    public int nGrille2;
}
