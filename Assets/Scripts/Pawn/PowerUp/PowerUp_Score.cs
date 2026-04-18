using UnityEngine;
[System.Serializable]
public class PowerUp_Score : PowerUp
{
    public int amountToAdd;

    public override void Apply( Pawn target )
    {
        if (target.health != null)
        {
            // Call its heal component
            target.controller.AddScore( amountToAdd );
        }
    }

    public override void Remove( Pawn target )
    {
        // TODO: Nothing. We don't do anything when removing a score powerup
    }
}