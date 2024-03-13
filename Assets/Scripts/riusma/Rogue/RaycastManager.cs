using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    GameObject objetTouche;
    public Transform posCam;
    [SerializeField] GameObject buttonPlay;
    [HideInInspector] public GameObject objSelec;
    GameObject objInst;
    public GameObject iconePerso;
    // Start is called before the first frame update
    

    void Update()
    {
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        if (hit.collider != null)
        {

            objetTouche = hit.transform.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                
                posCam.position = new Vector3(objetTouche.transform.position.x, 3, -10);
                iconePerso.transform.position = new Vector3(objetTouche.transform.position.x, objetTouche.transform.position.y - 1.5f, -0.15f);
                objSelec = objetTouche;
                if (objInst == null || objInst.transform.parent.gameObject != objSelec)
                {
                    Destroy(objInst);
                    objInst = Instantiate(buttonPlay, objSelec.transform);
                    objInst.transform.position += new Vector3(0,0,-0.1f);
                    objInst.GetComponentInChildren<ButtonScript>().lvlSelected = objSelec.GetComponent<SpriteRenderer>().sprite.name;
                }
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (objInst != null) Destroy(objInst.gameObject);
                objSelec = null;
            }
        }
    }
}
