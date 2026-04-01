using UnityEngine;
[System.Serializable]
public class PowerUp_HealthConsumable : PowerUp
{
    public float amountToHeal;

    public override void Apply( Pawn target )
    {
        // Heal the pawn in target
        // Check if the pawn we are "healing" has a health component
        Debug.Log("Healed!");
        if (target.health != null)
        {
            // Call its heal component
            target.health.Heal( amountToHeal );
        }
    }

    public override void Remove( Pawn target )
    {
        // TODO: Nothing. We don't do anything when removing a healing powerup
    }
}