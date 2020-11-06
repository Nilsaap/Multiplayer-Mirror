using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerModel : NetworkBehaviour
{
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        if (hasAuthority)
        {
            model.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
