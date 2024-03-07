using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CrystalEntity : Gamecrystal
{
    // Start is called before the first frame update
    void Start()
    {
        RegisterToCrystal();
    }

    // Update is called once per frame
    protected virtual void RegisterToCrystal()
    {
        int.TryParse(gameObject.name, out int valeur);
        Gamecrystal.RegisterToCrystal(valeur, gameObject.GetComponent<RawImage>());
    }
}
