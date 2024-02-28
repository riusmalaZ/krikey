using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class InterfaceEnnemis : MonoBehaviour
{
    public GameObject boutonPrefab; // Préfabriqué de bouton à utiliser
    public Transform panelBoutons; // Panneau où les boutons seront affichés

    public List<Unit> listeEnnemis = new();

    public BattleSysteme battleSysteme;

    public TextMeshProUGUI texts;

    public int nombreColonnes = 3;
    public float espaceEntreBoutons = 10f;

    void Start()
    {
        int ligneCourante = 0;
        int colonneCourante = 0;

        listeEnnemis = battleSysteme.unitsList;

        //RectTransform panelRect = panelBoutons.rect;

        // Parcours de la liste des ennemis
        foreach (Unit ennemi in listeEnnemis)
        {
            
                if(ennemi.Enemy){
                
                // Création d'un nouveau bouton
                GameObject nouveauBouton = Instantiate(boutonPrefab, panelBoutons);

                // Positionner le bouton dans la grille
                RectTransform boutonTransform = nouveauBouton.GetComponent<RectTransform>();
                boutonTransform.localPosition = new Vector2(colonneCourante * (boutonTransform.rect.width + espaceEntreBoutons),
                                                         -ligneCourante * (boutonTransform.rect.height + espaceEntreBoutons));

                // Incrémenter la colonne courante
                colonneCourante++;

                // Passer à la ligne suivante si nécessaire
                if (colonneCourante >= nombreColonnes)
                {
                    colonneCourante = 0;
                    ligneCourante++;
                }

                texts = nouveauBouton.GetComponentInChildren<TextMeshProUGUI>();

                Debug.Log("OUAIS OUAIS OUAIS");

                // Obtention du composant Text du bouton pour afficher le nom de l'ennemi
                //Text texteBouton = nouveauBouton.GetComponentInChildren<Text>();
                if (texts != null)
                {
                    texts.text = ennemi.name; // Vous devez définir une propriété "Nom" dans votre classe Unit
                }

                // Ajout d'un gestionnaire d'événements au bouton pour effectuer une action lorsque le bouton est cliqué
                Button boutonComponent = nouveauBouton.GetComponent<Button>();
                if (boutonComponent != null)
                {
                    boutonComponent.onClick.AddListener(() => BoutonEnnemiClique(ennemi));
                }
                }
        }
    }

    // Méthode appelée lorsqu'un bouton ennemi est cliqué
    void BoutonEnnemiClique(Unit ennemi)
    {
        // Insérez ici le code pour traiter le clic sur un ennemi spécifique
        Debug.Log("Le bouton de l'ennemi " + ennemi.name + " a été cliqué !");
    }
}

