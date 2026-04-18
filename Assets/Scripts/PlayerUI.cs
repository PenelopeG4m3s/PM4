using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public ControllerPlayer player;
    public TMP_Text lives;
    public TMP_Text score;
    public TMP_Text health;
    public TMP_Text respawn;
    public TMP_Text gameover;

    // Update is called once per frame
    void Update()
    {
        ResetEverything();

        if ( player.currentLives != 0 && player.currentRespawnTimer == 0 )
        {
            UpdateLives();
            UpdateScore();
            UpdateHealth();
        }
        if (player.currentRespawnTimer != 0)
        {
            UpdateRespawnTimer();
        }
        if (player.currentLives == 0)
        {
            UpdateGameOver();
        }
    }

    void ResetEverything()
    {
        lives.text = "";
        score.text = "";
        health.text = "";
        respawn.text = "";
        gameover.text = "";
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

    void UpdateRespawnTimer()
    {
        respawn.text = "Respawn in: " + player.currentRespawnTimer;
    }

    void UpdateGameOver()
    {
        gameover.text = "GAME OVER";
    }
}
