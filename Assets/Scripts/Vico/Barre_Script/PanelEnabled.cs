using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelEnabled : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        CheckButtonEvent();
    }

    public void CheckButtonEvent()
    {

        foreach (Transform childTransform in transform)
        {
            // Obtenir le GameObject à partir du Transform de l'enfant
            GameObject childGameObject = childTransform.gameObject;

            // Vérifier si le GameObject enfant a un composant Button attaché
            Button buttonComponent = childGameObject.GetComponent<Button>();
            if (buttonComponent != null)
            {
                // Vérifier si le bouton n'a aucun événement attaché
                if (buttonComponent.onClick.GetPersistentEventCount() == 0)
                {
                    // Désactiver le GameObject enfant
                    childGameObject.SetActive(false);
                }
            }
        }
    }
}