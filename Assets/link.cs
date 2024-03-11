using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class link : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB; 
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, pointA.position);
        lineRenderer.SetPosition(1, pointB.position);

    }
}
