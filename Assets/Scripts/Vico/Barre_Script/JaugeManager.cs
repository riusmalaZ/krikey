using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeManager : MonoBehaviour
{
    public GameObject[] PlayersIcon;

    public GameObject[] EnemysIcone;

    public GameObject[] AllIcons;

    public BattleSysteme battleSysteme;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < EnemysIcone.Length; i++)
        {
            if(i < battleSysteme.enemyUnitList.Count)
            {
                Debug.Log("gol");
                EnemysIcone[i].GetComponent<IconeJauge>().Icone = battleSysteme.enemyUnitList[i];
                EnemysIcone[i].GetComponent<RawImage>().texture = battleSysteme.enemyUnitList[i].Icone;
            }
            else
            {
                EnemysIcone[i].SetActive(false);
            }
            
        }

        for (int i = 0; i < PlayersIcon.Length; i++)
        {
            if(i < battleSysteme.enemyUnitList.Count)
            {
                Debug.Log("Mon");
                PlayersIcon[i].GetComponent<IconeJauge>().Icone = battleSysteme.playerUnitList[i];
                PlayersIcon[i].GetComponent<RawImage>().texture = battleSysteme.playerUnitList[i].Icone;
            }
            else
            {
                PlayersIcon[i].SetActive(false);
            }
            
        }
    }

    
}
