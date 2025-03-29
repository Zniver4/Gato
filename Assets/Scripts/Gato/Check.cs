using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Check : MonoBehaviour
{
    public static event Action<int> OnPress;

    public Button button;
    public TextMeshProUGUI buttonText;
    public string playerSide;
    string MyID;

    public int checkPosition;

    private GameManagerJs gameManager;

    GatoDb gatoDb;
    SetID setID;
    phpManager phpManager;

    private void Awake()
    {
        SetID.SetIDGame += SetMyID;
        StartCoroutine(GetGatoDB());
    }

    public void setSpace()
    {
        if (MyID == "id2" && gatoDb.actual == 2)
        {
            buttonText.text = "O";
            button.interactable = false;
            OnPress.Invoke(checkPosition);
        }

        if (MyID == "id1" && gatoDb.actual == 1)
        {
            buttonText.text = "X";
            button.interactable = false;
            OnPress.Invoke(checkPosition);
        }
    }

    void SetMyID(string setmyID)
    {
        MyID = setmyID;
        //print("Ckeck ID: " + MyID);
    }

    public void setGameManagerReference(GameManagerJs manager)
    {
        gameManager = manager;
    }

    IEnumerator GetGatoDB()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=2");
        yield return www.Send();

        gatoDb = JsonUtility.FromJson<GatoDb>(www.downloadHandler.text);

        /*Debug.Log("Ronda: " + gatoDb.round);
        Debug.Log("Jugador actual: " + gatoDb.actual);
        Debug.Log("Score1: " + gatoDb.score1);
        Debug.Log("Score2: " + gatoDb.score2);
        Debug.Log("Jugador 1: " + gatoDb.p1);
        Debug.Log("Jugador 2: " + gatoDb.p2);
        Debug.Log("Board: " + string.Join(", ", gatoDb.board));*/
    }
}
