using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCombat : MonoBehaviour
{
    // Indique l'objet qui sert de point de pivot à la caméra
    public Transform target; 

    // Défini la vitesse de rotation de la caméra
    [Range(0, 50)]
    public float rotationSpeed = 50.0f; 


    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        //Debug.Log("R");
        if (target != null)
        {
            float horizontalInput = 0f;

                // Défini l'input pour tourner à droite
            if (Input.GetKey(KeyCode.E)) 
            {
                // Va chercher la rotationSpeed à appliquer à l'input
                Debug.Log("E");
                horizontalInput = 1f * rotationSpeed;
            }       // Défini l'input pour tourner à
            else if (Input.GetKey(KeyCode.Q)) 
            {
                // Va chercher la rotationSpeed à appliquer à l'input
                Debug.Log("Q");
                horizontalInput = -1f * rotationSpeed;
            }

            transform.RotateAround(target.position, Vector3.up, horizontalInput * Time.deltaTime);
        }
    }
}
