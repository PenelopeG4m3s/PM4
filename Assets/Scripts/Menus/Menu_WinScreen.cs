using UnityEngine;

public class Menu_WinScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinMenu_Back ()
    {
        GameManager.instance.ActivateMainMenuScreen();
    }
}
