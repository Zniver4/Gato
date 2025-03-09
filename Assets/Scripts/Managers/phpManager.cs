using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class phpManager : MonoBehaviour
{
    int position;
    int actual = 1;
    string MyID;
    public TextMeshProUGUI turnText; // Asume que tienes un Text en el UI para mostrar el turno

    void Awake()
    {
        Check.OnPress += SetPosition;
        SetID.SetIDGame += ID;
    }

    void SetPosition(int chekPosition)
    {
        position = chekPosition;
        StartCoroutine(SetInPHP());
        actual++;
        print("Position in index: " + position);
    }

    void ID(string IDGame)
    {
        MyID = IDGame;
    }

    IEnumerator SetInPHP()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=3&id=" + MyID + "&pos=" + position + "&actual=" + actual);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Show Result as Text
            Debug.Log(www.downloadHandler.text);

            // Parse the JSON response to get the current turn
            var jsonResponse = www.downloadHandler.text;
            var response = JsonUtility.FromJson<ServerResponse>(jsonResponse);

            if (response.result == "OK")
            {
                // Update the UI with the current turn
                turnText.text = "Turno actual: " + response.actual;
            }
            else
            {
                Debug.Log("Error en el turno: " + response.result);
            }
        }
    }

    [System.Serializable]
    public class ServerResponse
    {
        public string result;
        public int actual;
    }
}
