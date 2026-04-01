using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    public InputActionAsset inputActions;

    public override void MakeDecisions()
    {
        // TODO: Write this function to make the decisions
        Vector2 movementVector = inputActions["Move"].ReadValue<Vector2>();
        pawn.Move(new Vector2(0, movementVector.y));
        pawn.Rotate(new Vector2(movementVector.x, 0));

        if (inputActions["Shoot"].IsPressed())
        {
            pawn.Shoot();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        // Enable my input actions
        inputActions.Enable();

        // Add this to the list of players
        GameManager.instance.players.Add(this);
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
}