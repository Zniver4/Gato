using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class jsManager : MonoBehaviour
{
    GatoDbJs gatoDbjs;
    SetID setID;

    int position;
    string MyID;



    void Awake()
    {
        Check.OnPress += SetAction;
        SetID.SetIDGame += ID;
    }

    private void Start()
    {
        StartCoroutine(StartDb());
    }

    void SetAction(int boardPosition)
    {
        position = boardPosition;
        StartCoroutine(SetPlay());
    }

    void ID(string IDGame)
    {
        MyID = IDGame;
    }

    IEnumerator StartDb()
    {
        using UnityWebRequest GameStart = UnityWebRequest.Get("http://localhost:8080/action/init");
        yield return GameStart.SendWebRequest();

        if (GameStart.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ Game Started Correctly: " + GameStart.downloadHandler.text);

            StartCoroutine(GetDataBase());
        }
        else
        {
            Debug.LogError("Error, Game Couldn't Start: " + GameStart.error);
        }
    }

    IEnumerator GetDataBase()
    {
        while (true)
        {
            using UnityWebRequest JsDataBase = UnityWebRequest.Get("http://localhost:8080/action/data");
            {
                yield return JsDataBase.SendWebRequest();

                if (JsDataBase.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("JSON recibido: " + JsDataBase.downloadHandler.text);

                    // ✅ Base de datos actualizada
                    gatoDbjs = JsonUtility.FromJson<GatoDbJs>(JsDataBase.downloadHandler.text);
                }
                else
                {
                    Debug.LogError("Error al descargar JSON: " + JsDataBase.error);
                }

                yield return new WaitForSeconds(2);
            }
        }
    }

    IEnumerator SetPlay()
    {
        using UnityWebRequest SetPlay = UnityWebRequest.Get("http://localhost:8080/turn/" + MyID + "/" + position);
        yield return SetPlay.SendWebRequest();
        Debug.Log("Play Sent");
    }

    private void Start()
    {
        StartCoroutine(StartDb());
    }

    void SetAction(int boardPosition)
    {
        position = boardPosition;
        StartCoroutine(SetPlay());
    }

    void ID(string IDGame)
    {
        MyID = IDGame;
    }

    IEnumerator StartDb()
    {
        using UnityWebRequest GameStart = UnityWebRequest.Get("http://localhost:8080/action/init");
        yield return GameStart.SendWebRequest();

        if (GameStart.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("✅ Game Started Correctly: " + GameStart.downloadHandler.text);

            StartCoroutine(GetDataBase());
        }
        else
        {
            Debug.LogError("Error, Game Couldn't Start: " + GameStart.error);
        }
    }

    IEnumerator GetDataBase()
    {
        while (true)
        {
            using UnityWebRequest JsDataBase = UnityWebRequest.Get("http://localhost:8080/action/data");
            {
                yield return JsDataBase.SendWebRequest();

                if (JsDataBase.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("JSON recibido: " + JsDataBase.downloadHandler.text);

                    // ✅ Base de datos actualizada
                    gatoDbjs = JsonUtility.FromJson<GatoDbJs>(JsDataBase.downloadHandler.text);
                }
                else
                {
                    Debug.LogError("Error al descargar JSON: " + JsDataBase.error);
                }

                yield return new WaitForSeconds(2);
            }
        }
    }

    IEnumerator SetPlay()
    {
        using UnityWebRequest SetPlay = UnityWebRequest.Get("http://localhost:8080/turn/" + MyID + "/" + position);
        yield return SetPlay.SendWebRequest();
        Debug.Log("Play Sent");
    }


}
