using UnityEngine;

public class ControllerAI_Vulture : ControllerAI
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
                // Check for transitions
                if ( (CanSee( target.gameObject ) || CanHear( target.gameObject )) && target != null )
                {
                    if ( target.GetComponent<Health>() != null )
                    {
                        if ( target.GetComponent<Health>().currentHealth > 5 )
                        {
                            ChangeState( AISTATES.Flee );
                        }
                    }
                    ChangeState( AISTATES.Chase );
                }
            break;
            // Chase
            case AISTATES.Chase:
                // DO WORK
                DoChase();

                // Check for transitions
                if ( !CanSee( target.gameObject ) && !CanHear( target.gameObject ) )
                {
                    ChangeState( AISTATES.Idle );
                }
                else
                {
                    Health health = target.GetComponent<Health>();
                    if ( health.currentHealth > 5 )
                    {
                        ChangeState( AISTATES.Flee );
                    }
                    else
                    {
                        Vector3 vectorToTarget = pawn.transform.position - target.position;
                        if ( vectorToTarget.magnitude < maxShootingDistance )
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
                Vector3 vectorToFlee = pawn.transform.position - target.position;
                if ( vectorToFlee.magnitude >= fleeDistance )
                {
                    ChangeState( AISTATES.Idle );
                }
            break;
            // Chase And Shoot
            case AISTATES.ChaseAndShoot:
                // DO WORK
                DoChaseAndShoot();

                // Check for transitions
                if ( !CanSee( target.gameObject ) && !CanHear( target.gameObject ) )
                {
                    ChangeState( AISTATES.Idle );
                }
                else
                {
                    Health health = target.GetComponent<Health>();
                    if ( health.currentHealth > 5 )
                    {
                        ChangeState( AISTATES.Flee );
                    }
                    else
                    {
                        // check if player is within shooting distance
                        Vector3 vectorTarget = pawn.transform.position - target.position;
                        if ( maxShootingDistance < vectorTarget.magnitude )
                        {
                            ChangeState( AISTATES.Chase );
                        }
                    }
                }
            break;
        }

        base.MakeDecisions();
    }

    public override void DoIdle()
    {
        // TODO: Whatever idle does
        base.DoIdle();
    }

    public override void DoChase()
    {
        // TODO: Whatever Chase does
        base.DoChase();
    }

    public override void DoFlee()
    {
        base.DoFlee();
    }

    public override void DoChaseAndShoot()
    {
        base.DoChaseAndShoot();
    }
}
