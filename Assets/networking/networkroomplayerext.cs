using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class networkroomplayerext : NetworkRoomPlayer
{
    [SyncVar]
    public string playerName;

    public static event Action<networkroomplayerext, string> OnMessage;

    [Command]
    public void CmdSend(string message)
    {
        if (message.Trim() != "")
            RpcReceive(message.Trim());
    }

    [ClientRpc]
    public void RpcReceive(string message)
    {
        OnMessage?.Invoke(this, message);
    }

    [SyncVar(hook = nameof(handelreadychange))]
    public bool isReady;

    [SyncVar(hook = nameof(handelnameset))]
    public string usernamename;

    GameObject readyButton;

    Button button;

    public void handelreadychange(bool oldValue, bool newValue) => updateDisplay();
    public void handelnameset(string oldValue, string newValue) => updateDisplay();

    // Update is called once per frame
    void Update()
    {

    }

    public void updateDisplay()
    {
        networklobbymanagerext manager = GameObject.Find("Manager").GetComponent<networklobbymanagerext>();
        if (!hasAuthority)
        {
            foreach (networkroomplayerext player in manager.roomSlots)
            {
                if (player.hasAuthority)
                {
                    player.updateDisplay();
                    break;
                }
            }

            return;

        }
        readyButton = GameObject.Find("Buttons");
        GameObject players = GameObject.Find("Players");
        for (int i = 0; i < readyButton.transform.childCount; i++)
        {
            readyButton.transform.GetChild(i).GetComponent<Button>().interactable = false;
            readyButton.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "Not Ready";
        }
        readyButton.transform.GetChild(index).GetComponent<Button>().interactable = true;


        for (int i = 0; i < manager.roomSlots.Count; i++)
        {
            networkroomplayerext playerScript = manager.roomSlots[i].GetComponent<networkroomplayerext>();
            players.transform.GetChild(i).GetComponent<Text>().text = playerScript.usernamename;

            if (playerScript.isReady)
            {
                readyButton.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "Ready";
            }
            else
            {
                readyButton.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "Not Ready";
            }
        }

    }

    public override void OnStartAuthority()
    {
        Cmdsetname(PlayerPrefs.GetString("name"));

    }


    public override void OnClientEnterRoom()
    {
        base.OnClientEnterRoom();

        updateDisplay();
        readyButton = GameObject.Find("Buttons");
        Debug.Log(index);
        button = readyButton.transform.GetChild(index).GetComponent<Button>();
        Debug.Log(button.name);
        button.onClick.AddListener(delegate { readyUp(); });
    }

    [Command]
    private void Cmdsetname(string displayName)
    {

        usernamename = displayName;
        playerName = displayName;
        updateDisplay();
    }

    [Command]
    private void CmdReadyup()
    {
        
        isReady = readyToBegin;
        Debug.Log(isReady);
    }

    public override void ReadyStateChanged(bool _, bool newReadyState)
    {
        base.ReadyStateChanged(_, newReadyState);
        updateDisplay();
    }

    public void readyUp()
    {
        if (NetworkClient.active && isLocalPlayer)
        {
            readyToBegin = !readyToBegin;
            CmdChangeReadyState(readyToBegin);
            CmdReadyup();
            updateDisplay();
        }
    }
}
