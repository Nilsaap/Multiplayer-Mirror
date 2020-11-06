using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class networklobbymanagerext : NetworkRoomManager
{
    public TMP_InputField joinIpadress;
    public Text ipadresstext;
    public string address;
    public void host()
    { 

        address = networkAddress;
        StartHost();
    }

    //public override void ServerChangeScene(string newSceneName) {
    //    networkAddress = hostIpadress.text;
    //
    //}
    public override void OnServerSceneChanged(string newSceneName)
    {
        if(newSceneName == GameplayScene)
        {
            StartCoroutine(startChat());
        }
        if (newSceneName == RoomScene)
        {
            ipadresstext = GameObject.FindGameObjectWithTag("Ip").GetComponent<Text>();
            ipadresstext.text = address;
        }
    }


    public void Client()
    {
        //todo: get text for ip adress
        networkAddress = joinIpadress.text;
        StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator startChat()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("Chat Manager").GetComponent<Chatmanager>().networkAddress = networkAddress;
        GameObject.Find("Chat Manager").GetComponent<Chatmanager>().StartHost();
    }
}
