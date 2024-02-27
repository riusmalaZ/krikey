using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeCombat : MonoBehaviour
{

    public Slider jauge;

    public float speed;

    public float valeur;

    public float valMax; 
    // Start is called before the first frame update
    void Start()
    {
        valMax = jauge.maxValue;
        valeur = 0;
        jauge.value = valeur;
    }

    // Update is called once per frame
    void Update()
    {
        if(valeur < valMax)
        {
            valeur = valeur + 1 * (speed*Time.deltaTime);

            jauge.value = valeur;
        }

        if(valeur >= valMax)
            valeur = 0;
    }
}
