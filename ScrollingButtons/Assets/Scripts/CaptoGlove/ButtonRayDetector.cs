using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRayDetector : MonoBehaviour
{
    [SerializeField] float rayLength = 2f;
    [SerializeField] LayerMask buttonLayer;
    [SerializeField] GameObject button;
    bool btnDetected;
    private void Update()
    {
        RayCast();
    }

    private void RayCast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, rayLength, buttonLayer))
        {
            button = hit.transform.gameObject;
            btnDetected = true;
        }
        else
        {
            button = null;
            btnDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }

    public bool ButtonDetected => btnDetected;
    public GameObject Button => button;
}
