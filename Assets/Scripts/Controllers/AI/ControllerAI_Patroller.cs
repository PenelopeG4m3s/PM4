using UnityEngine;
using Unity.Collections;

public class ControllerAI_Patroller : ControllerAI
{
    public Vector3 lastPosition = new Vector3(0.0f,0.0f,0.0f);
    public float maxChaseDistance;
    public Vector3[] patrolPath = new Vector3[] {};
    int currentPatrolPath = 0;

    public override void Start()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        target = player[0].transform;

        //Vector3[] patrolPath = { new Vector3( 5.0f, 0.0f, 5.0f), new Vector3( -5.0f, 0.0f, 5.0f), new Vector3( -5.0f, 0.0f, -5.0f), new Vector3( 5.0f, 0.0f, -5.0f) };
    }

    public override void MakeDecisions()
    {
        // Look at what state we are in
        switch (currentState)
        {
            // Patrol
            case AISTATES.Patrol:
                // DO WORK
                DoPatrol();

                // Check for transitions
                // If the player can see the 
                if ( CanSee( target.gameObject ) || CanHear( target.gameObject ) )
                {
                    lastPosition = pawn.transform.position;
                    ChangeState( AISTATES.Chase );
                }
            break;
            // Chase
            case AISTATES.Chase:
                // DO WORK
                DoChase();

                // Check for transitions
                
                // if the player is no longer detected
                if ( !CanSee( target.gameObject ) && !CanHear( target.gameObject ) )
                {
                    ChangeState( AISTATES.Patrol);
                }
                else // player is still detected
                {
                    // check if the ai is past chase distance
                    Vector3 vectorToPosition = pawn.transform.position - lastPosition;
                    if ( maxChaseDistance < vectorToPosition.magnitude )
                    {
                        ChangeState( AISTATES.Flee );
                    }
                    else // the ai is not past chase distance
                    {
                        Vector3 vectorToTheTarget = pawn.transform.position - target.position;
                        if ( maxShootingDistance >= vectorToTheTarget.magnitude )
                        {
                            ChangeState( AISTATES.ChaseAndShoot );
                        }
                    }
                }
            break;
            // Flee
            case AISTATES.Flee:
                // DO WORK
                DoFlee();

                // Check for transitions
                Vector3 vectorToTarget = pawn.transform.position - lastPosition;
                // AI is back to original position
                if ( vectorToTarget.magnitude < .5f )
                {
                    ChangeState( AISTATES.Patrol );
                }
            break;
            // Chase And Shoot
            case AISTATES.ChaseAndShoot:
                // DO WORK
                DoChaseAndShoot();

                // Check for transitions
                if ( (!CanSee( target.gameObject ) && !CanHear( target.gameObject )) || ( target.gameObject == null ) )
                {
                    ChangeState( AISTATES.Patrol );
                }
                else // player is still detected
                {
                    // check if the ai is past chase distance
                    Vector3 vectorToPosition = pawn.transform.position - lastPosition;
                    if ( maxChaseDistance < vectorToPosition.magnitude )
                    {
                        ChangeState( AISTATES.Flee );
                    }
                    else
                    {
                        // check if player is within shooting distance
                        Vector3 vectorTarget = pawn.transform.position - target.position;
                        if ( maxShootingDistance >= vectorTarget.magnitude )
                        {
                            ChangeState( AISTATES.ChaseAndShoot );
                        }
                    }
                }
            break;
        }

        base.MakeDecisions();
    }

    public override void DoPatrol()
    {
        // Check if the player is at the current point on the list
        Vector3 vectorToPath = pawn.transform.position - patrolPath[currentPatrolPath];
        if ( vectorToPath.magnitude < 0.5f )
        {
            int arrayLength = patrolPath.Length;
            if ( currentPatrolPath < arrayLength - 1.0 )
            {
                currentPatrolPath += (int)1.0;
            }
            else // loop around the array
            {
                currentPatrolPath = (int)0.0;
            }
        }
        else // player is not at the point so it goes to the point
        {
            Seek( patrolPath[ currentPatrolPath ] );
        }
    }

    public override void DoChase()
    {
        // TODO: Whatever Chase does
        base.DoChase();
    }

    public override void DoFlee()
    {
        Seek( lastPosition );
    }

    public override void DoChaseAndShoot()
    {
        base.DoChaseAndShoot();
    }

}
