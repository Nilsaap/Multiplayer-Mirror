using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Health : MonoBehaviour
{
    public float playerHealth;
    public Image Bar;
    public Text text;
    public GameObject player;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = playerHealth.ToString();
        Bar.fillAmount = playerHealth / 100;
        if (playerHealth <= 0)
        {
            //gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
            gameObject.GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            Debug.Log("you died");
        }
    }
    public void takeDamage(float damage)
    {
       playerHealth -= damage;
        if(playerHealth <= 0)
        {
            //gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
            gameObject.GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            Debug.Log("you died");
        }
    }
}
