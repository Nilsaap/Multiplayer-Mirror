using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Chatmanager : NetworkManager
{
    public string PlayerName { get; set; }

    public void SetHostname(string hostname)
    {
        networkAddress = hostname;
    }

    public playerchatwindow chatWindow;

    public class CreatePlayerMessage : MessageBase
    {
        public string name;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        // tell the server to create a player with this name
        conn.Send(new CreatePlayerMessage { name = PlayerName });
    }

    void OnCreatePlayer(NetworkConnection connection, CreatePlayerMessage createPlayerMessage)
    {
        // create a gameobject using the name supplied by client
        GameObject playergo = Instantiate(playerPrefab);
        playergo.GetComponent<Chatplayer>().playerName = createPlayerMessage.name;

        // set it as the player
        NetworkServer.AddPlayerForConnection(connection, playergo);

        chatWindow.gameObject.SetActive(true);
    }
}
