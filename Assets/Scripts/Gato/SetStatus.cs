using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetStatus : MonoBehaviour
{
    public Button[] Board;

    GatoDb gatoDb;

    private void Start()
    {
        StartCoroutine(GetStatus());
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
