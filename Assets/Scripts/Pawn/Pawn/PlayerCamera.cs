using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera myCamera;
    public Pawn pawn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the name of the camera
        gameObject.name = "Player" + pawn.controller.gameObject.GetComponent<ControllerPlayer>().playerNumber + "Camera";

        // Check if the camera exists
        if ( gameObject.GetComponent<Camera>() != null )
        {
            // Set the camera variable
            myCamera = gameObject.GetComponent<Camera>();
        }
    }

    void Update()
    {
        // Check if the pawn exists
        if ( pawn != null )
        {
            // Have the camera follow the player
            FollowPlayer();
        }
    }

    public void SetPawn( Pawn newPawn )
    {
        pawn = newPawn;
    }

    public void SetCamera( int playerNumber, int playerCount)
    {
        int Col = Mathf.CeilToInt(Mathf.Sqrt(playerCount));
        int Row = Mathf.CeilToInt( ( playerCount * 1.0f ) / Col);

        Rect newRect = new Rect();
        float newX = (((playerNumber - 1.0f) % Col) / Col);
        float newY = (((Mathf.FloorToInt( ( playerNumber - 1.0f ) / Col ) / ( 1.0f * Row ) ) / -1.0f ) + ( 1.0f - ( 1.0f / Row ) ) );
        newRect.x = newX;
        newRect.y = newY;
        newRect.width = (1.0f / Col);
        newRect.height = (1.0f / Row);

        myCamera.rect = newRect;
    }

    public void FollowPlayer()
    {
        // TODO: Follow the player
        // TODO: Grab the spot that the camera needs to be positioned at
        Vector3 cameraLookAtPosition = pawn.gameObject.transform.position + (pawn.gameObject.transform.forward * 10.0f );

        Vector3 cameraPosition = pawn.gameObject.transform.position + (pawn.gameObject.transform.forward * 5.0f * -1.0f ) + (pawn.gameObject.transform.right * 1.0f ) + (pawn.gameObject.transform.up * 3.0f);
        
        Vector3 vectorToPawn = transform.position - cameraPosition;
        float distanceToPawn = vectorToPawn.magnitude;

        transform.position = Vector3.MoveTowards( transform.position, cameraPosition, distanceToPawn * 10.0f * Time.deltaTime);
        

        transform.LookAt(cameraLookAtPosition);
        Quaternion lookRotation = Quaternion.LookRotation( cameraLookAtPosition );
        transform.rotation = Quaternion.RotateTowards( transform.rotation, lookRotation, 2.0f * Time.deltaTime );
        //Vector3 positionToMoveTowards = pawn.gameObject.transform.position + (transform.forward * 5.0f);
        //transform.position = positionToMoveTowards;

        // TODO: Grab the spot that the camera needs to look at
        // Find the vector to target
        //Vector3 vectorToTarget = pawn.gameObject.transform.position - transform.position;

        //Vector3 flippedVectorToTarget = vectorToTarget;
        //flippedVectorToTarget.Normalize();

        // Find the quaternion (look instructions) on how to look down that vector
        //Quaternion lookRotation = Quaternion.LookRotation( vectorToTarget + ( flippedVectorToTarget * 2 ) );

        // Rotate just a little towards that new rotation
        //transform.rotation = Quaternion.RotateTowards( transform.rotation, lookRotation, 40.0f * Time.deltaTime );
    }

}