
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Paths;
    List<List<GameObject>> entirePath = new List<List<GameObject>>();
    [SerializeField] PositionData positionData;
    GameObject pointPerso;
    public RaycastManager raycastManager;
    


    void Start()
    { 
        InitEntirePath();
        pointPerso = entirePath[positionData.position.x][positionData.position.y];
        raycastManager.iconePerso.transform.position = new Vector3(pointPerso.transform.position.x, pointPerso.transform.position.y - 1.5f, -0.15f);
        raycastManager.posCam.position = new Vector3 (pointPerso.transform.position.x, 0, -10);
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
        
    }
}
