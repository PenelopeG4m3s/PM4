using UnityEngine;

public class Menu_Credits : MonoBehaviour
{
    public void CreditsMenu_Back()
    {
        GameManager.instance.ActivateMainMenuScreen();
    }
}
