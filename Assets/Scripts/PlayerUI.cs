using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public ControllerPlayer player;
    public TMP_Text lives;
    public TMP_Text score;
    public TMP_Text health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLives();
        UpdateScore();
        UpdateHealth();
    }

    void UpdateLives()
    {
        lives.text = "Lives: " + player.currentLives;
    }

    void UpdateScore()
    {
        score.text = "Score: " + player.score;
    }

    void UpdateHealth()
    {
        health.text = "Health: " + player.pawn.health.currentHealth + "/" + player.pawn.health.maxHealth;
    }
}
