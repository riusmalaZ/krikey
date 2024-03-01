using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCombat : MonoBehaviour
{
    // Indique l'objet qui sert de point de pivot à la caméra
    public Transform target; 

    // Défini la vitesse de rotation de la caméra
    [Range(0, 50)]
    public float rotationSpeed = 3.5f; 


    private Vector3 offset;

    bool direction = false;

    float horizontalInput;

    void Start()
    {
        offset = transform.position - target.position;
        horizontalInput = 0f;
    }

    void Update()
    {
        
        if (target != null)
        {
            
            

                // Défini l'input pour tourner à droite
            if (Input.GetKey(KeyCode.E) == false) 
            {
                // Va chercher la rotationSpeed à appliquer à l'input
                Debug.Log("E");
                horizontalInput = 1f * rotationSpeed;
                direction = true;
            }       // Défini l'input pour tourner à
            else if (Input.GetKey(KeyCode.Q) == false) 
            {
                // Va chercher la rotationSpeed à appliquer à l'input
                Debug.Log("Q");
                horizontalInput = -1f * rotationSpeed;
                direction = true;
            }

            transform.RotateAround(target.position, Vector3.up, horizontalInput * Time.deltaTime);
        }
    }
}
