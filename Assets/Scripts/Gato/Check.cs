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

    private GameManager gameManager;

    GatoDbJs gatoDb;
    SetID setID;
    //phpManager phpManager;

    private void Start()
    {
        StartCoroutine(GetDataBasejs());
        SetID.SetIDGame += SetMyID;
    }

    public void setGameManagerReference(GameManager manager)
    {
        gameManager = manager;
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
        print("Ckeck ID: " + MyID);
        Debug.Log("GatoDbJs Actual: " + gatoDb.actual);
    }

    IEnumerator GetDataBasejs()
    {
        using UnityWebRequest JsDataBase = UnityWebRequest.Get("http://localhost:8080/action/data");
        {
            yield return JsDataBase.SendWebRequest();

            if (JsDataBase.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("JSON recibido: " + JsDataBase.downloadHandler.text);

                // ✅ Base de datos actualizada
                gatoDb = JsonUtility.FromJson<GatoDbJs>(JsDataBase.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error al descargar JSON: " + JsDataBase.error);
            }
        }
    }
}
