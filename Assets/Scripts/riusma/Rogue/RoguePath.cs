using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguePath : MonoBehaviour
{
    public GameObject PreviousPoint;
    public List<GameObject> Path;

    void Start()
    {
        if (PreviousPoint != null) Path.Insert(0, PreviousPoint);
    }
}
