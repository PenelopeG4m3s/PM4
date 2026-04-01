using UnityEngine;

public class PickUp_HealthConsumable : PickUp
{
    public PowerUp_HealthConsumable powerup;

    public override void OnTriggerEnter( Collider other )
    {
        // TODO: Check the other object has a PowerUpManager
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
