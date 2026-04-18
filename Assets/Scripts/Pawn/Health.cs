using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    public AudioClip healSound;
    public AudioManage audioManage;
    //private AudioSource audioSource;
    [HideInInspector] public Pawn damageDealer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        audioManage = GetComponent<AudioManage>();

        // Tanks start max health
        currentHealth = maxHealth;
    }

    public void TakeDamage ( float amount, Pawn pawn )
    {
        currentHealth -= amount;

        // Play sound
        if ( currentHealth > 0 && amount > 0)
        {
            audioManage.PlayAudio(takeDamageSound);
        }

        // Keep health between 0 and max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Check for Death
        CheckDie();

        damageDealer = pawn;

    }

    public void Heal ( float amount )
    {
        currentHealth += amount;

        // Play sound
        if ( (currentHealth - amount) != Mathf.Clamp(currentHealth,0,maxHealth) )
        {
            audioManage.PlayAudio(healSound);
        }

        // Keep health between 0 and max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Check for Death
        CheckDie();
    }

    public void UpgradeHealth( float amount )
    {
        // TODO: increase max health
        maxHealth += amount;

        // Keep health between 0 and max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Check for Death
        CheckDie();
    }

    public void Die (  )
    {
        // lose a life
        Controller myController = gameObject.GetComponent<Pawn>().controller;
        myController.currentLives -= 1;
        myController.currentRespawnTimer = myController.maxRespawnTimer;
        Destroy(gameObject);

        audioManage.PlayAudio(deathSound);

        // give the other person points
        if (damageDealer != null)
        {
            damageDealer.controller.AddScore(100);
        }
    }

    public void CheckDie (  )
    {
        // Check for Death
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
