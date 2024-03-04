using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public GameObject objetTouche;
    public Transform posCam;
    [SerializeField] GameObject buttonPlay;
    public Material[] materials;
    [HideInInspector] public GameObject objSelec;
    public GameObject objInst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        if (hit.collider != null)
        {

            objetTouche = hit.transform.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                
                posCam.position = new Vector3(objetTouche.transform.position.x, 0, -10);
                if (objSelec != null) objSelec.GetComponent<SpriteRenderer>().material = materials[0];
                objetTouche.GetComponent<SpriteRenderer>().material = materials[1];
                objSelec = objetTouche;
                if (objInst == null || objInst.transform.parent.gameObject != objSelec)
                {
                    Destroy(objInst);
                    objInst = Instantiate(buttonPlay, objSelec.transform);
                }
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (objSelec != null) objSelec.GetComponent<SpriteRenderer>().material = materials[0];
                if (objInst != null) Destroy(objInst.gameObject);
                objSelec = null;
            }
        }
    }
}
