using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [HideInInspector] public Mover mover;
    [HideInInspector] public Health health;

    [HideInInspector] public Controller controller;
    public abstract void Move(Vector3 directionToMove);
    public abstract void Rotate(Vector3 directionToRotate);
    public abstract void RotateTowards(Vector3 position, float turnSpeed);

    public abstract void Shoot();

    public float moveSpeed;
    public float turnSpeed;
    public Controller GetController () { return controller; }

    public virtual void Start()
    {
        // Get the mover component
        mover = GetComponent<Mover>();

        // Get the health component
        health = GetComponent<Health>();
    }
}
