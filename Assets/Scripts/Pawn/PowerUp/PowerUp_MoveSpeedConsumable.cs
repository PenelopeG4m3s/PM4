using UnityEngine;
[System.Serializable]
public class PowerUp_MoveSpeedConsumable : PowerUp
{
    public float speedBoostAmount;

    public override void Apply( Pawn target )
    {
        // Increase the pawn's move speed
        target.moveSpeed += speedBoostAmount;
    }

    public override void Remove( Pawn target )
    {
        // Increase the pawn's move speed
        target.moveSpeed -= speedBoostAmount;
    }
}
