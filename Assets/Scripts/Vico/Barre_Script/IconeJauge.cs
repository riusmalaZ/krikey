using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconeJauge : MonoBehaviour
{
    public RectTransform bar; // Référence à la barre
    
    public Unit Icone;

    private float maxWidth; // Largeur maximale de la barre
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        Icone = gameObject.GetComponent<Unit>();
        maxWidth = bar.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
         // Calculer la position de l'icône en fonction de la jauge
        float newY = Mathf.Lerp(0f, maxWidth, Icone.Progression / 100f);

        // Déplacer l'icône vers la nouvelle position
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}
