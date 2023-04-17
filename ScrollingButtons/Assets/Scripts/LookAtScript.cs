using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, transform.rotation.z);
    }
}
