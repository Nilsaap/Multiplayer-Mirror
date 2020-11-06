using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float force = 20f;
    public float radius = 10;
    public ForceMode forceMode;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bigBoom()
    {
        Vector3 dir = transform.position;
        rigidbody.AddForce(dir, forceMode);
    }
}
