using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class phpManager : MonoBehaviour
{
    int position;
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
        //print("Position in index: " + position);
    }

    void ID(string IDGame)
    {
        MyID = IDGame;
    }

    IEnumerator SetInPHP()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=3&id=" + MyID + "&pos=" + position);
        yield return www.Send();
    }

    [System.Serializable]
    public class ServerResponse
    {
        public string result;
        public int actual;
    }
}
