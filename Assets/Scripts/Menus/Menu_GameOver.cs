using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Menu_GameOver : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        string newText = "Scores:\n";
        for (int i = 0; i < GameManager.instance.scores.Length; i++ )
        {
            newText += "Player " + ( i + 1 ) + ": " + GameManager.instance.scores[i] +"\n";
        }
        scoreText.text = newText;
    }

    public void GameOverMenu_Back ()
    {
        GameManager.instance.ActivateMainMenuScreen();
    }

    public void GameOverMenu_Retry ()
    {
        GameManager.instance.ActivateStartGameScreen();
    }
}
