using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecrystal : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary<int, RawImage> cells = new Dictionary<int, RawImage>();
    void Awake()
    {
        
    }

    

    public static void RegisterToCrystal(int indice, RawImage image)
    {
        if(!cells.ContainsKey(indice))
        {
            cells.Add(indice, image);
            
        }
    }

    
}
