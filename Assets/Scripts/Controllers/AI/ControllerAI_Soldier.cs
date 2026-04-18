using UnityEngine;

public class ControllerAI_Soldier : ControllerAI
{
    public override void Start()
    {
        base.Start();
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
                if ( CanSee( target.gameObject ) || CanHear( target.gameObject ) )
                {
                    ChangeState( AISTATES.Chase );
                }
            break;
            // Chase
            case AISTATES.Chase:
                // DO WORK
                DoChase();

                // Check for transitions
                // if health is less than 5
                Health health = pawn.gameObject.GetComponent<Health>();

                bool ChangeFlee = false;
                if ( health != null )
                {
                    if ( health.currentHealth < 5 )
                    {
                        ChangeFlee = true;
                    } else {
                        ChangeFlee = false;
                    }
                    //ChangeFlee = ( health.currentHealth < 5 );
                }

                if ( ChangeFlee)
                {
                    ChangeState( AISTATES.Flee );
                }
                else
                {
                    // if the player is no longer detected
                    if ( !CanSee( target.gameObject ) && !CanHear( target.gameObject ) )
                    {
                        ChangeState( AISTATES.Idle);
                    }
                    else
                    {
                        Vector3 vectorToTarget = pawn.transform.position - target.position;
                        if ( maxShootingDistance >= vectorToTarget.magnitude )
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
                if ( (!CanSee( target.gameObject ) && !CanHear( target.gameObject )) || ( target == null ) )
                {
                    ChangeState( AISTATES.Idle );
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
