using UnityEngine;

public class PawnTank : Pawn
{
    private ShooterTank shooter;
    public float shootForce;

    public override void Start()
    {
        // Save my tank in my GameManager
        GameManager.instance.tanks.Add(this);

        // Get the shooter component
        shooter = GetComponent<ShooterTank>();

        // Do what all pawns do
        base.Start();
    }

    public void OnDestroy()
    {
        // Remove my tank from the GameManager list
        GameManager.instance.tanks.Remove(this);
    }

    public override void Move(Vector3 directionToMove)
    {
        mover.Move(directionToMove, moveSpeed);
    }

    public override void Rotate(Vector3 directionToRotate)
    {
        mover.Rotate(directionToRotate, turnSpeed);
    }

    public override void Shoot()
    {
        shooter.Shoot();
    }

    public override void RotateTowards(Vector3 position, float turnSpeed)
    {
        mover.RotateTowards( position, turnSpeed );
    }
}