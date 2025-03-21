using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
class ChatDB
{
    public string text;
}

public class ChatManager : MonoBehaviour
{
    [SerializeField]
    List<ChatDB> messageList = new List<ChatDB>();

    public TMP_InputField AnimeUserMessage;
    public TextMeshProUGUI textMeshProUGUI;
    public string baseUrl = "http://localhost/u3d_chat/chat.php/";


    public void SendMessageToChat(string text)
    {
        ChatDB newMessage = new ChatDB { text = text };
        messageList.Add(newMessage);
        UpdateChatUI();
        Debug.Log("Mensaje recibido: " + text);
    }

    private void UpdateChatUI()
    {
        textMeshProUGUI.text = "";
        foreach (var msg in messageList)
        {
            textMeshProUGUI.text += msg.text + "\n";
        }
    }

    public void getRooms()
    {
        StartCoroutine(GetRequest(baseUrl + "?action=1"));
    }

    public void getMessages(string room)
    {
        messageList.Clear();
        StartCoroutine(GetRequest(baseUrl + "?action=2&room=" + room));
    }

    public void SendMessageToChat()
    {
        ChatDB message0 = new ChatDB();

        textMeshProUGUI.text = message0.text;

        messageList.Add(message0);
    }

    public void Message()
    {
        string room = "anime",
            user = "prueba",
            message = AnimeUserMessage.text.ToString();
        Debug.Log(message);
        StartCoroutine(GetRequest(baseUrl + "?action=3&room=" + room + "&username=" + user + "&message=" + message));
        AnimeUserMessage.text = "";
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la petición: " + webRequest.error);
            }
            else
            {
                string responseText = webRequest.downloadHandler.text;
                SendMessageToChat(responseText); // Ahora sí enviamos el texto al chat
            }

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(uri + "\nError: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(uri + "\nHTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(uri + "\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
