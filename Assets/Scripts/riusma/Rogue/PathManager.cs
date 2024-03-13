
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public List<GameObject> Paths;
    List<List<GameObject>> entirePath = new List<List<GameObject>>();
    [SerializeField] PositionData positionData;
    GameObject pointPerso;
    public RaycastManager raycastManager;
    GameObject Grille1;
    GameObject Grille2;
    [SerializeField] GameObject[] Grilles1;
    [SerializeField] GameObject[] Grilles2;


    void Start()
    {
        if (positionData.nGrille1<0)
        {
            positionData.nGrille1 = Random.Range(0, Grilles1.Length);
            positionData.nGrille2 = Random.Range(0, Grilles2.Length);
        }
        
        Grille1 = Grilles1[positionData.nGrille1];
        Grille2 = Grilles2[positionData.nGrille2];
        foreach (GameObject grille in Grilles1) if (grille != Grille1) grille.SetActive(false);
        foreach (GameObject grille in Grilles2) if (grille != Grille2) grille.SetActive(false);
        InitListPath();
        InitEntirePath();
        pointPerso = entirePath[positionData.position.x][positionData.position.y];
        raycastManager.iconePerso.transform.position = new Vector3(pointPerso.transform.position.x, pointPerso.transform.position.y - 1.5f, -0.15f);
        raycastManager.posCam.position = new Vector3 (pointPerso.transform.position.x, 3, -10);
        if (positionData.position.y != entirePath[positionData.position.x].Count - 1)
        {
            entirePath[positionData.position.x][positionData.position.y + 1].GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            foreach (List<GameObject> liste in entirePath)
            {
                if (liste[0] == pointPerso)
                { 
                    liste[1].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

    }

    
    public void NextPoint()
    {
        int x = 0;
        int y = 0;
        while (entirePath[x][y] != raycastManager.objSelec)
        {
            if (y == entirePath[x].Count - 1)
            {
                y = 0;
                x++;
            }
            else y++;
            positionData.position = new Vector2Int(x, y);
        }
    }

    void InitListPath()
    {
        for (int i = 0; i < Grille1.transform.childCount - 1; i++)
        {
            Paths.Add(Grille1.transform.GetChild(i + 1).gameObject);
        }
        for (int i = 0; i < Grille2.transform.childCount - 1; i++)
        {
            Paths.Add(Grille2.transform.GetChild(i + 1).gameObject);
        }
    }

    void InitEntirePath()
    {
        List<RoguePath> listeDataPath = new List<RoguePath>();
        for (int i = 0; i < Paths.Count; i++)
        {
            listeDataPath.Add(Paths[i].GetComponent<RoguePath>());
        }

        for (int i = 0; i < listeDataPath.Count; i++)
        {
            entirePath.Add(listeDataPath[i].Path);
        }
        
        for (int i = 0; i < entirePath.Count; i++)
        {
            for (int j = 0; j < entirePath[i].Count; j++)
            {
                LineRenderer lineRenderer = entirePath[i][j].GetComponent<LineRenderer>();
                if (j != entirePath[i].Count - 1 && j != 0)
                {
                    lineRenderer.positionCount = 2;
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;
                    lineRenderer.SetPosition(0, new Vector3(entirePath[i][j].transform.position.x, entirePath[i][j].transform.position.y, 0));
                    lineRenderer.SetPosition(1, new Vector3(entirePath[i][j+1].transform.position.x, entirePath[i][j+1].transform.position.y, 0));
                }
                else
                {
                    lineRenderer.positionCount = 4;
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;
                    bool find1 = false;
                    foreach (List<GameObject> listObj in entirePath)
                    {
                        if (listObj[0] == entirePath[i][j])
                        { 
                            
                            if (find1 == false)
                            {
                                lineRenderer.SetPosition(0, new Vector3(entirePath[i][j].transform.position.x, entirePath[i][j].transform.position.y, 0));
                                lineRenderer.SetPosition(1, new Vector3(listObj[1].transform.position.x, listObj[1].transform.position.y, 0));
                                find1 = true;
                            }
                            lineRenderer.SetPosition(2, new Vector3(entirePath[i][j].transform.position.x, entirePath[i][j].transform.position.y, 0));
                            lineRenderer.SetPosition(3, new Vector3(listObj[1].transform.position.x, listObj[1].transform.position.y, 0));

                        }
                    }
                }
            }
        }
    }
}
