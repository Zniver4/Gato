using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Check : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonText;
    public string playerSide;

    private GameManager gameManager;

    public void setSpace()
    {
        buttonText.text = gameManager.GetPlayerSide();
        button.interactable = false;
        gameManager.EndTurn();
    }

    public void setGameManagerReference(GameManager manager)
    {
        gameManager = manager;
    }
}
