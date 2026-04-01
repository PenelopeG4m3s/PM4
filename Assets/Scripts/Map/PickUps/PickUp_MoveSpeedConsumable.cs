using UnityEngine;

public class PickUp_MoveSpeedConsumable : PickUp
{
    public PowerUp_MoveSpeedConsumable powerup;

    public override void OnTriggerEnter( Collider other )
    {
        // Check if the other object has a PowerUpManager
        PowerUpManager otherManager = other.GetComponent<PowerUpManager>();

        if ( otherManager != null )
        {
            // Add powerup
            otherManager.Add(powerup);

            // Destroy this object
            Destroy(gameObject);
        }

        base.OnTriggerEnter(other);
    }
}
