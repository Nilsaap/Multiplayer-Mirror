using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_facing_billboard : MonoBehaviour
{
    public Camera cam;
    
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up);
    }
}
