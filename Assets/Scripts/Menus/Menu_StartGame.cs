using UnityEngine;
using UnityEngine.UI;
using TMPro;

//public enum RandomType { Random, Seeded, MapOfTheDay };

public class Menu_StartGame : MonoBehaviour
{
    [Header("Variables")]
    public int playerCount = 1;
    public RandomType randomType;
    public int seed = 0;
    [Header("Text")]
    public TMP_Text playerCountText;
    public TMP_Dropdown mapType;
    public TMP_Text seedText;
    public Button seedAdd;
    public Button seedSubtract;
    public Color enabledText;
    public Color disabledText;

    public void Start()
    {
        playerCountText.text = "" + playerCount;
        seedText.text = "" + seed;
        DisableSeed();
    }

    public void StartGame_Start()
    {
        GameManager.instance.ActivateGameplayScreen( playerCount, randomType, seed );
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

    public void StartGame_PlayerSeedAdd()
    {
        seed += 1;
        seed = (int)Mathf.Clamp(seed, 0, 10000000000000000000);
        seedText.text = "" + seed;
    }

    public void StartGame_PlayerSeedSubtract()
    {
        seed -= 1;
        seed = (int)Mathf.Clamp(seed, 0, 10000000000000000000);
        seedText.text = "" + seed;
    }

    public void StartGame_MapType()
    {
        DisableSeed();

        switch(mapType.value)
        {

            // Random
            case 0:
                Debug.Log("Random");
                randomType = RandomType.Random;
            break;
            // Seeded
            case 1:
                Debug.Log("Seeded");
                randomType = RandomType.Seeded;
                EnableSeed();
            break;
            // Map of the Day
            case 2:
                Debug.Log("Map of the Day");
                randomType = RandomType.MapOfTheDay;
            break;
        }
    }

    public void StartGame_Back()
    {
        GameManager.instance.ActivateMainMenuScreen();
    }

    void DisableSeed()
    {
        seedAdd.interactable = false;
        seedSubtract.interactable = false;
        seedText.color = disabledText;
    }

    void EnableSeed()
    {
        seedAdd.interactable = true;
        seedSubtract.interactable = true;
        seedText.color = enabledText;
    }
}
