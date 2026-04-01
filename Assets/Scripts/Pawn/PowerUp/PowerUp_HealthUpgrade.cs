using UnityEngine;
[System.Serializable]
public class PowerUp_HealthUpgrade : PowerUp
{
    public float amountToUpgrade;

    public override void Apply( Pawn target )
    {
        // Heal the pawn in target
        // Check if the pawn we are "healing" has a health component
        Debug.Log("Health Upgraded!");
        if (target.health != null)
        {
            // Call its heal component
            target.health.UpgradeHealth( amountToUpgrade );
        }
    }

    public override void Remove( Pawn target )
    {
        // TODO: Nothing. We don't do anything when removing a healing powerup
    }
}