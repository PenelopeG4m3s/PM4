using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    public InputActionAsset inputActions;
    public int playerNumber;
    public PlayerCamera myCamera;
    public PlayerInput playerInput;

    public override void MakeDecisions()
    {
        Vector2 movementVector = playerInput.actions["Move"].ReadValue<Vector2>();
        pawn.Move(new Vector2(0, movementVector.y));
        pawn.Rotate(new Vector2(movementVector.x, 0));

        if (playerInput.actions["Shoot"].IsPressed())
        {
            pawn.Shoot();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        // Do base start stuff
        base.Start();

        //inputActions.Enable();

        // Enable my input actions
        if (playerNumber == 2)
        {
            //inputActions.Disable();
            //inputActions.Enable();
            playerInput.SwitchCurrentActionMap( "Player2" );
        }
        else
        {
            //inputActions2.Enable();
            //inputActions2.Disable();
            playerInput.SwitchCurrentActionMap( "Player1" );
        }

        //SetActionMap();

        // Add this to the list of players
        GameManager.instance.players.Add(this);
        GameManager.instance.scores[playerNumber-1] = score;
    }

    public void SetActionMap()
    {
        if (playerNumber == 2)
        {
            var player1Map = inputActions.FindActionMap("Player1");
            player1Map.Disable();
            var player2Map = inputActions.FindActionMap("Player2");
            player2Map.Enable();
        } 
        else
        {
            var player1Map = inputActions.FindActionMap("Player1");
            player1Map.Enable();
            var player2Map = inputActions.FindActionMap("Player2");
            player2Map.Disable();
        }
    }

    public void OnDestroy()
    {
        // Remove this from the list of player
        GameManager.instance.players.Remove(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        // Do what the parent class (Controller) does on update
        base.Update();
    }

    public override void Respawn()
    {
        // Get a list of all the spawn points
        Transform playerSpawn = GameManager.instance.playerSpawnPoints[Random.Range(0,GameManager.instance.playerSpawnPoints.Count)].transform;
            
        // create the pawn
        Pawn tempTankPawn = GameManager.instance.SpawnTank( GameManager.instance.playerPawnPrefab, Vector3.zero );
            
        // Possess the pawn
        Possess( tempTankPawn );

        // Set the pawn to the spawn point
        tempTankPawn.transform.position = playerSpawn.position;
            
        // Set the parent to the gameplay object
        tempTankPawn.transform.parent = transform.parent;

        // Set the camera
        myCamera.SetPawn( tempTankPawn );
    }

    public override void AddScore( int amount )
    {
        base.AddScore(amount);
        GameManager.instance.scores[playerNumber-1] = score;
    }
}