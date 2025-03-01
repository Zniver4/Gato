using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI[] textList;
    private string playerSide;
    public TextMeshProUGUI gameOverText;

    int turns = 0;

    private void Awake()
    {
        SetGameManagerReferenceOnButtons();
        playerSide = "X";
    }

    void SetGameManagerReferenceOnButtons()
    {
        for (int i = 0; i < textList.Length; i++)
        {
            textList[i].GetComponentInParent<Check>().setGameManagerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        turns++;

        if (textList[0].text == playerSide && textList[1].text == playerSide && textList[2].text == playerSide)
        {
            GameOver();
        }

        if (textList[3].text == playerSide && textList[4].text == playerSide && textList[5].text == playerSide)
        {
            GameOver();
        }

        if (textList[6].text == playerSide && textList[7].text == playerSide && textList[8].text == playerSide)
        {
            GameOver();
        }

        if (textList[0].text == playerSide && textList[3].text == playerSide && textList[6].text == playerSide)
        {
            GameOver();
        }

        if (textList[1].text == playerSide && textList[4].text == playerSide && textList[7].text == playerSide)
        {
            GameOver();
        }

        if (textList[2].text == playerSide && textList[5].text == playerSide && textList[8].text == playerSide)
        {
            GameOver();
        }

        if (textList[0].text == playerSide && textList[4].text == playerSide && textList[8].text == playerSide)
        {
            GameOver();
        }

        if (textList[2].text == playerSide && textList[4].text == playerSide && textList[6].text == playerSide)
        {
            GameOver();
        }

        ChangeSides();

        if(turns >= 9)
        {
            gameOverText.gameObject.SetActive(true);

            gameOverText.text = "ItÅLs a Draw!!!";
        }
    }

    void GameOver()
    {
        for(int i = 0; i < textList.Length; i++)
        {
            textList[i].GetComponentInParent<Button>().interactable = false;
        }

        gameOverText.gameObject.SetActive(true);
        gameOverText.text = playerSide + " WINS!!!";
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }
}
