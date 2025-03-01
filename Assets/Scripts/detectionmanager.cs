using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionmanager : MonoBehaviour
{
    private int[,] board = new int[3, 3];
    private int currentPlayer = 1;

    void Start()
    {
        ResetBoard();
    }

    public void CellClicked(int x, int y)
    {
        if (board[x, y] == 0)
        {
            board[x, y] = currentPlayer;
            currentPlayer = 3 - currentPlayer;
            CheckWinner();
        }
    }

    void ResetBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = 0;
            }
        }
    }

    void CheckWinner()
    {
        // Verifica las filas, columnas y diagonales para encontrar un ganador
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                // Ganador encontrado
                Debug.Log("Winner: " + board[i, 0]);
                ResetBoard();
                return;
            }
        }
        // Otros chequeos...
    }
}
