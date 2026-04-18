using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public ControllerPlayer player;
    public TMP_Text lives;
    public TMP_Text score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLives();
        UpdateScore();
    }

    void UpdateLives()
    {
        lives.text = "Lives: " + player.currentLives;
    }

    void UpdateScore()
    {
        score.text = "Score: " + player.score;
    }
}
