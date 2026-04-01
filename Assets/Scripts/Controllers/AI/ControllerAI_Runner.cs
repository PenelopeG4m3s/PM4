using UnityEngine;

public class ControllerAI_Runner : ControllerAI
{
    public override void Start()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        target = player[0].transform;
    }

    public override void MakeDecisions()
    {
        // Look at what state we are in
        switch (currentState)
        {
            // Idle
            case AISTATES.Idle:
                // DO WORK
                DoIdle();

                // Check for transitions
                if ( target != null && CanHear( target.gameObject ) )
                {
                    Vector3 vectorToTarget = pawn.transform.position - target.position;
                    if ( vectorToTarget.magnitude < fleeDistance )
                    {
                        ChangeState( AISTATES.Flee );
                    }
                    else
                    {
                        ChangeState( AISTATES.TurnAndShoot );
                    }
                }
            break;
            // Flee
            case AISTATES.Flee:
                // DO WORK
                DoFlee();

                // Check for transitions
                Vector3 vectorToFlee = pawn.transform.position - target.position;
                if (CanHear(target.gameObject))
                {
                    // Check if the ai is far enough away from the player
                    if ( vectorToFlee.magnitude >= fleeDistance )
                    {
                        ChangeState( AISTATES.TurnAndShoot );
                    }
                }
                else // ai cannot hear the pawn
                {
                    ChangeState( AISTATES.Idle );
                }
            break;
            // Turn And Shoot
            case AISTATES.TurnAndShoot:
                // DO WORK
                DoTurnAndShoot();

                // Check for transitions
                Vector3 vectorFlee = pawn.transform.position - target.position;

                // Check if ai is not far away enough from the player
                if ( vectorFlee.magnitude < fleeDistance )
                {
                    ChangeState( AISTATES.Flee );
                }
                if ( !CanHear( target.gameObject ) )
                {
                    ChangeState( AISTATES.Idle );
                }
            break;
        }

        base.MakeDecisions();
    }

    public override void DoIdle()
    {
        base.DoIdle();
    }

    public override void DoFlee()
    {
        base.DoFlee();
    }

    public override void DoTurnAndShoot()
    {
        base.DoTurnAndShoot();
    }
}
