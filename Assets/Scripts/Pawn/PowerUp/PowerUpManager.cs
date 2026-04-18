using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PowerUpManager : MonoBehaviour
{
    public List <PowerUp> powerups;
    //public List <PowerUp> bee;
    //public List <Pawn> tanks;
    //public float bees;
    private Pawn pawn;

    public void Start()
    {
        // Get the pawn this PowerUpManager 
        pawn = GetComponent<Pawn>();

        // Initialize the list of powerups
        powerups = new List<PowerUp>();
    }

    public void Update()
    {
        // Update the countdown (lifespan) for every powerup
        UpdatePowerUpLifespans();
        //Debug.Log(powerups.Count);

        // TODO: Check for expired powerups and remove them
        //CheckForExpiredPowerUps();


        // TODO: TODO: Way Later - Not in this class - this is where you would check for and apply "over time" effects
    }

    private void LateUpdate ()
    {
        CheckForExpiredPowerUps();
    }

    public void UpdatePowerUpLifespans()
    {
        foreach ( PowerUp powerup in powerups )
        {
            powerup.lifespan -= Time.deltaTime;
        }
    }

    public void CheckForExpiredPowerUps()
    {
        // First make a list of the powerups we need to remove
        List<PowerUp> powerupsToRemove = new List<PowerUp>();

        foreach ( PowerUp powerup in powerups)
        {
            if (powerup.lifespan <= 0)
            {
                powerupsToRemove.Add(powerup);
            }
        }

        // Then remove them from the (main) list
        // -- this way, you aren't iterating through the main list when you remove them

        foreach ( PowerUp powerup in powerupsToRemove )
        {
            Remove(powerup);
        }
    }

    public void Add( PowerUp powerup )
    {
        // Apply the powerup's effects
        powerup.Apply(pawn);

        // Add it to our list
        powerups.Add(powerup); 
    }

    public void Remove( PowerUp powerup )
    {
        // Remove the powerups effects
        powerup.Remove(pawn);

        // Remove the powerup from the list
        powerups.Remove(powerup);
    }

}
