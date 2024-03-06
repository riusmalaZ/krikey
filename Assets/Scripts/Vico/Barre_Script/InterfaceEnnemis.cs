using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class InterfaceEnnemis : MonoBehaviour
{
    public GameObject boutonPrefab; // Préfabriqué de bouton à utiliser
    public RectTransform panelBoutonsEnnemis; // Panneau où les boutons seront affichés

    public RectTransform panelBoutonsPlayer; // Panneau où les boutons seront affichés

    public Dictionary<Unit, List<GameObject> > listeEnnemis = new();

    public BattleSysteme battleSysteme;

    public TextMeshProUGUI texts;

    public int nombreColonnes = 3;
    public float espaceEntreBoutons = 10f;

    void Start()
    {
        int ligneCourante = 0;
        int colonneCourante = 0;

        RectTransform ActualPanel = panelBoutonsPlayer;

        listeEnnemis = battleSysteme.unitsList;

        

        // Parcours de la liste des ennemis
        foreach (Unit Units in listeEnnemis.Keys)
        {
                if(Units.Enemy)
                    ActualPanel = panelBoutonsEnnemis;
                else    
                    ActualPanel = panelBoutonsPlayer;
                
                // Création d'un nouveau bouton
                GameObject nouveauBouton = Instantiate(boutonPrefab, ActualPanel);

                listeEnnemis[Units].Add(nouveauBouton);

                // Positionner le bouton dans la grille
                RectTransform boutonTransform = nouveauBouton.GetComponent<RectTransform>();
                boutonTransform.anchorMin = new Vector2(0, 1); // Coin supérieur gauche
                boutonTransform.anchorMax = new Vector2(0, 1); // Coin supérieur gauche
                boutonTransform.pivot = new Vector2(0, 1); // Coin supérieur gauche
                boutonTransform.anchoredPosition = new Vector2(colonneCourante * (boutonTransform.rect.width + espaceEntreBoutons),
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

                //Debug.Log("OUAIS OUAIS OUAIS");

                // Obtention du composant Text du bouton pour afficher le nom de l'ennemi
                //Text texteBouton = nouveauBouton.GetComponentInChildren<Text>();
                if (texts != null)
                {
                    texts.text = Units.unitName; // Vous devez définir une propriété "Nom" dans votre classe Unit
                }

                // Ajout d'un gestionnaire d'événements au bouton pour effectuer une action lorsque le bouton est cliqué
                Button boutonComponent = nouveauBouton.GetComponent<Button>();
                if (boutonComponent != null && Units.Enemy)
                {
                    boutonComponent.onClick.AddListener(() => BoutonEnnemiClique(Units));
                }
                
        }
    }

    // Méthode appelée lorsqu'un bouton ennemi est cliqué
    void BoutonEnnemiClique(Unit ennemi)
    {
        //battleSysteme.PlayerAttack(ennemi);
    }
}

