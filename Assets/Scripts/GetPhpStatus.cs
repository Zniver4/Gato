using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class GatoDb
{
    public int round;
    public int actual;
    public int score1;
    public int score2;
    public int[] board;
    public string p1;
    public string p2;
}

public class GetPhpStatus : MonoBehaviour
{
    public Button[] Board;

    GatoDb gatoDb;  // ✅ Se mantiene la referencia de clase

    private void Start()
    {
        gatoDb = new GatoDb();

        StartCoroutine(GetPHPStatus());
        StartCoroutine(GetStatus());
    }

    IEnumerator GetPHPStatus()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato/gato.php?action=2"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("JSON recibido: " + www.downloadHandler.text);

                // ✅ Ahora sí actualiza la variable de clase
                gatoDb = JsonUtility.FromJson<GatoDb>(www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error al descargar JSON: " + www.error);
            }
        }
    }

    IEnumerator GetStatus()
    {
        while (gatoDb.board == null)
        {
            Debug.Log("⏳ Esperando datos de PHP...");
            yield return new WaitForSeconds(1);
        }

        Debug.Log("✅ Datos recibidos, actualizando tablero...");
        SetStatusBoard();
        yield return new WaitForSeconds(2f);
    }



    public void SetStatusBoard()
    {
        if (gatoDb.board == null)
        {
            Debug.LogError("❌ ERROR: Intentando actualizar pero 'board' es null.");
            return;
        }

        Debug.Log("✅ Actualizando tablero con: " + string.Join(", ", gatoDb.board));

        for (int arrayPos = 0; arrayPos < Board.Length && arrayPos < gatoDb.board.Length; arrayPos++)
        {
            if (Board[arrayPos] == null)
            {
                Debug.LogError($"❌ ERROR: Botón en posición {arrayPos} es null. Verifica el Inspector.");
                continue;
            }

            if (gatoDb.board[arrayPos] == 1)
            {
                Board[arrayPos].GetComponentInChildren<Text>().text = "X";
                Board[arrayPos].interactable = false;
            }
            else if (gatoDb.board[arrayPos] == 2)
            {
                Board[arrayPos].GetComponentInChildren<Text>().text = "O";
                Board[arrayPos].interactable = false;
            }
        }
        Debug.Log("✅ Tablero actualizado correctamente.");
    }

}
