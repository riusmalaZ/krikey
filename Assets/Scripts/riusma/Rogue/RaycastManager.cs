using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    GameObject objetTouche;
    public Transform posCam;
    [SerializeField] GameObject buttonPlay;
    [HideInInspector] public GameObject objSelec;
    GameObject objInst;
    public GameObject iconePerso;
    Vector3 posInit;
    bool setup = false;
    // Start is called before the first frame update
    
    void Start()
    {
        posInit = posCam.position;
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
                
                posInit = new Vector3(objetTouche.transform.position.x, 3, -10);
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

        if (setup) posCam.position = Vector3.Lerp(posCam.position, posInit, 3 * Time.deltaTime);
    }

    public void Setup(Vector3 position)
    {
        setup = true;
        posCam.position = position;
        posInit = position;
    }
}
