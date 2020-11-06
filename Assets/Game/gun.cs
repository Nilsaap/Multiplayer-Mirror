using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public ParticleSystem muzzelFlash;
    public Camera cam;
    public float damage;
    public float range = 100f;
    public float explosionForce = 25;
    public float explosionRadius = 25;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();

            muzzelFlash.Play();
        }
        
    }

    public void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            
            Health tareget;
            if(tareget = hit.transform.GetComponent<Health>())
            {
                tareget.GetComponent<Health>().takeDamage(damage);
            }

            if (hit.transform.gameObject.GetComponent<Rigidbody>()) {
                hit.transform.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce*50, hit.point, explosionRadius);
            }

        }
    }
}
