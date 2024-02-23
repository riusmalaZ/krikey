using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("----Allies----")]
    [SerializeField] GameObject[] AllyTeam;
    [SerializeField] GameObject[] SpawnsAlly;
    [Header("----Ennemies----")]
    [SerializeField] GameObject[] EnnemyTeam;
    [SerializeField] GameObject[] SpawnsEnnemy;
    GameObject objInst;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AllyTeam.Length; i++)
        {
            objInst = Instantiate(AllyTeam[i]);
            objInst.transform.position = SpawnsAlly[i].transform.position + new Vector3(0, 1, 0);
        }
        for (int i = 0; i < EnnemyTeam.Length; i++)
        {
            objInst = Instantiate(EnnemyTeam[i]);
            objInst.transform.position = SpawnsEnnemy[i].transform.position + new Vector3(0, 1, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
