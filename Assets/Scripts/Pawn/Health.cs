using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage ( float amount )
    {
        Debug.Log("Damage Taken: "+amount);
        // TODO: Take Damage
        currentHealth -= amount;

        // Keep health between 0 and max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Check for Death
        CheckDie();
    }

    public void Heal ( float amount )
    {
        // TODO: Heal
        currentHealth += amount;

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
        Debug.Log( gameObject.name + " has moved on to a better place");
        Destroy(gameObject);
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
