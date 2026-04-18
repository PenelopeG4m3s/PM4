using UnityEngine;
using UnityEngine.UI;
using TMPro;

//public enum RandomType { Random, Seeded, MapOfTheDay };

public class Menu_StartGame : MonoBehaviour
{
    [Header("Variables")]
    public int playerCount;
    public RandomType randomType;
    public int seed;
    [Header("Text")]
    public TMP_Text playerCountText;

    public void Start()
    {
        playerCountText.text = "" + playerCount;
    }

    public void StartGame_Start()
    {
        GameManager.instance.ActivateGameplayScreen( playerCount, randomType );
    }

    public void StartGame_PlayerCountAdd()
    {
        playerCount += 1;
        playerCount = Mathf.Clamp(playerCount, 1, 2);
        playerCountText.text = "" + playerCount;
    }

    public void StartGame_PlayerCountSubtract()
    {
        playerCount -= 1;
        playerCount = Mathf.Clamp(playerCount, 1, 2);
        playerCountText.text = "" + playerCount;
    }

    public void StartGame_Back()
    {
        GameManager.instance.ActivateMainMenuScreen();
    }
}
