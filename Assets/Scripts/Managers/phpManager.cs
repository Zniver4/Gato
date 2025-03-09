using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class phpManager : MonoBehaviour
{
    int position;
    int actual = 1;

    string MyID;


    void Awake()
    {
        Check.OnPress += SetPosition;
        SetID.SetIDGame += ID;
    }

    // Update is called once per frame
    void Update()
    {
        
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

            //Or Retrive Results as Binary Data
            byte[] results = www.downloadHandler.data;
        }
    }
}
