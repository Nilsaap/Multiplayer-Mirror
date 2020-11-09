using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class playerchatwindow : MonoBehaviour
{
    static readonly ILogger logger = LogFactory.GetLogger(typeof(playerchatwindow));

    public InputField chatMessage;
    public Text chatHistory;
    public Scrollbar scrollbar;

    public void Awake()
    {
        networkroomplayerext.OnMessage += OnPlayerMessage;
    }

    void OnPlayerMessage(networkroomplayerext player, string message)
    {
        string prettyMessage = player.isLocalPlayer ?
            $"<color=red>{player.playerName}: </color> {message}" :
            $"<color=blue>{player.playerName}: </color> {message}";
        AppendMessage(prettyMessage);
        logger.Log(message);
    }

    public void OnSend()
    {
        if (chatMessage.text.Trim() == "")
            return;

        // get our player
        networkroomplayerext player = NetworkClient.connection.identity.GetComponent<networkroomplayerext>();

        // send a message
        player.CmdSend(chatMessage.text.Trim());

        chatMessage.text = "";
    }

    internal void AppendMessage(string message)
    {
        StartCoroutine(AppendAndScroll(message));
    }

    IEnumerator AppendAndScroll(string message)
    {
        chatHistory.text += message + "\n";

        // it takes 2 frames for the UI to update ?!?!
        yield return null;
        yield return null;

        // slam the scrollbar down
        scrollbar.value = 0;
    }
}
