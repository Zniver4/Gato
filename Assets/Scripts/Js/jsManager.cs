using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class GatoDbjs
{
    public int round;
    public int actual;
    public int score1;
    public int score2;
    public int[] board;
    public string p1;
    public string p2;
}


public class jsManager : MonoBehaviour
{
    /*public Button[] Board;*/
    GatoDbjs gatoDbjs;

    public void Start()
    {
        StartCoroutine(initDb());
        StartCoroutine(getDb());
    }
    IEnumerator initDb()
    {

        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/action/init"))
        {
            yield return www.SendWebRequest();

            gatoDbjs = JsonUtility.FromJson<GatoDbjs>(www.downloadHandler.text);

            Debug.Log("se obtuvo base de datos: " + gatoDbjs.actual);
            Debug.Log("Tablero: " + string.Join(", ", gatoDbjs.board));
        }
    }
    IEnumerator getDb()
    {

        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/action/db"))
        {
            yield return www.SendWebRequest();

            gatoDbjs = JsonUtility.FromJson <GatoDbjs> (www.downloadHandler.text);

            Debug.Log("se obtuvo base de datos: " + gatoDbjs.actual);
            Debug.Log("Tablero: " + string.Join(", ", gatoDbjs.board));
        }
    }


}
