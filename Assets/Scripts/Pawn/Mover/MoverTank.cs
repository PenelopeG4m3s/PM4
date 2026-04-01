using UnityEngine;

public class MoverTank : Mover
{
    private Pawn pawn;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        //pawn = GetComponent<Pawn>();
    }

    public override void Move(Vector2 moveDirection, float moveSpeed)
    {
        Vector3 moveVector = new Vector3(moveDirection.x, 0, moveDirection.y) * (moveSpeed * Time.deltaTime);
        moveVector = transform.TransformDirection(moveVector);
        
        rb.MovePosition(rb.position + (moveVector * (moveSpeed * Time.deltaTime)));
    }

    public override void Rotate(Vector2 rotateDirection, float turnSpeed)
    {
        float rotationAmount = rotateDirection.x;
        rotationAmount *= (turnSpeed);
        rotationAmount *= Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }

    public override void RotateTowards(Vector3 position, float turnSpeed)
    {
        // Find the vector to target
        Vector3 vectorToTarget = position - transform.position;

        // Find the quaternion (look instructions) on how to look down that vector
        Quaternion lookRotation = Quaternion.LookRotation(vectorToTarget);

        // Rotate just a little towards that new rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }


    
}
