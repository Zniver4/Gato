using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Check : MonoBehaviour
{
    public static event Action<int> OnPress;

    public Button button;
    public TextMeshProUGUI buttonText;
    public string playerSide;
    string MyID;

    public int checkPosition;
    public int turn = 1;

    private GameManager gameManager;
    SetID setID;

    private void Awake()
    {
        SetID.SetIDGame += SetMyID;
    }

    public void setSpace()
    {
        if(MyID == "id2" && turn % 2 != 0)
        {
            buttonText.text = "O";
            button.interactable = false;
            OnPress.Invoke(checkPosition);
            turn++;
            gameManager.EndTurn();
        }

        if (MyID == "id1" && turn % 2 == 0)
        {
            buttonText.text = "X";
            button.interactable = false;
            OnPress.Invoke(checkPosition);
            turn++;
            print(turn);
            gameManager.EndTurn();
        }
    }

    void SetMyID(string setmyID)
    {
        MyID = setmyID;
        print("Ckeck ID: " + MyID);
    }

    public void setGameManagerReference(GameManager manager)
    {
        gameManager = manager;
    }
}
