using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Prueba : MonoBehaviour
{
    GatoDb gatoDb;

    void Start()
    {
        StartCoroutine(pruebaDB());
    }

    IEnumerator pruebaDB()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=2");
        yield return www.Send();

        gatoDb = JsonUtility.FromJson<GatoDb>(www.downloadHandler.text);

        Debug.Log("Ronda: " + gatoDb.round);
        Debug.Log("Jugador actual: " + gatoDb.actual);
        Debug.Log("Score1: " + gatoDb.score1);
        Debug.Log("Score2: " + gatoDb.score2);
        Debug.Log("Jugador 1: " + gatoDb.p1);
        Debug.Log("Jugador 2: " + gatoDb.p2);
        Debug.Log("Board: " + string.Join(", ", gatoDb.board));
    }
}
