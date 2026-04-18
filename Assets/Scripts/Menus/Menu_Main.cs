using UnityEngine;

public class Menu_Main : MonoBehaviour
{
    public void MainMenu_Start()
    {
        GameManager.instance.ActivateStartGameScreen();
    }

    public void MainMenu_Options()
    {
        GameManager.instance.ActivateOptionsScreen();
    }

    public void MainMenu_Credits()
    {
        GameManager.instance.ActivateCreditsScreen();
    }

    public void MainMenu_Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
