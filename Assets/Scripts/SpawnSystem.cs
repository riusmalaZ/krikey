using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] GameObject[] AllyTeam;
    [SerializeField] GameObject[] Spawns;
    GameObject objInst;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AllyTeam.Length; i++)
        {
            objInst = Instantiate(AllyTeam[i]);
            objInst.transform.position = Spawns[i].transform.position + new Vector3(0, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
