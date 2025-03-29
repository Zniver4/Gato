using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerJs : MonoBehaviour
{
    public TextMeshProUGUI[] textList;
    public TextMeshProUGUI gameOverText;

    private string playerSide;
    private string playerID;

    SetID setID;

    int turns = 0;

    private void Awake()
    {
        SetID.SetIDGame += SetMySide;
    }

    void SetGameManagerReferenceOnButtons()
    {
        for (int i = 0; i < textList.Length; i++)
        {
            textList[i].GetComponentInParent<Check>().setGameManagerReference(this);
        }
    }


    IEnumerator TrowStatus()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=2&p1=1");
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

    IEnumerator Throw()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=3&id=id2&pos=4");
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

    void SetMySide(string MyID)
    {
        playerID = MyID;

        if (playerID == "id1")
        {
            playerSide = "X";
        }

        else { playerSide = "O"; }

        print(playerSide);
    }
}
