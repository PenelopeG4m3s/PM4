using UnityEngine;

public class ControllerAI : Controller
{
    public enum AISTATES { Idle, Chase, Flee, ChaseAndShoot, TurnAndShoot, Patrol, FindHealthpack, Rest }
    
    public AISTATES currentState;
    public Transform target;
    public float fleeDistance = 10.0f;
    public float lastStateChange;
    public float hearingDistance = 1.0f;
    public float visionDistance = 10.0f;
    public float FOVAngle = 60.0f;
    public float maxShootingDistance = 5.0f;

    public override void Start()
    {
        GameManager.instance.enemies.Add(this);
    }

    public override void MakeDecisions() {}

    // Function for changing states
    public void ChangeState( AISTATES newState )
    {
        currentState = newState;
        lastStateChange = Time.time;
    }

    // Define what each state does
    public virtual void DoIdle()
    {
        // TODO: Later, we can add some idle animations, but for now, do nothing!

    }

    public void Seek(Vector3 position)
    {
        // Turn towards the target we are chasing
        pawn.RotateTowards( position, pawn.turnSpeed );

        // Move forward
        pawn.Move(new Vector2( 0, 1 ));
    }

    public virtual void DoChase()
    {
        Seek(target.position);
    }

    public virtual void DoFlee()
    {
        // Pick a point a given instance away from the player!
        // Find vector to the player.
        Vector3 vectorToTarget = pawn.transform.position - target.position;
        
        float distanceToPlayer = vectorToTarget.magnitude;

        // Reverse it!
        Vector3 flippedVectorToTarget = vectorToTarget;

        // Find the distance to flee
        flippedVectorToTarget.Normalize();

        float percentOfFleeDistance = ( distanceToPlayer / fleeDistance );
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;
        float newFleeDistance = flippedPercentOfFleeDistance * fleeDistance;

        Vector3 targetPosition = pawn.transform.position + ( flippedVectorToTarget * newFleeDistance );
        Seek(targetPosition);
    }

    public virtual void DoChaseAndShoot()
    {
        Seek(target.position);
        pawn.Shoot();
    }

    public virtual void DoTurnAndShoot()
    {
        // Turn towards the target we are chasing
        pawn.RotateTowards( target.position, pawn.turnSpeed );
        // Shoot
        pawn.Shoot();
    }

    public virtual void DoPatrol() {}

    public bool CanSee( GameObject target )
    {
        RaycastHit hit;
        // TODO: Field of View Check


        // Line of Sight Check
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        if ( Physics.Raycast( pawn.transform.position, vectorToTarget, out hit, visionDistance ))
        {
            if (hit.collider.gameObject == target.gameObject)
            {
                return true;
            }

        }
        return false;

    }


    public bool CanHear( GameObject target )
    {
        // Check if our target has *NoiseMaker*
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();
        if (targetNoiseMaker == null) return false;

        // If so, are they making noise (>0)
        if ( targetNoiseMaker.noiseVolume > 0 )
        {
            // If so, is the distance between the two centers smaller than the radii added together
            float totalDistance = Vector3.Distance( target.transform.position, pawn.transform.position );
            //Debug.Log(totalDistance);
            if ( totalDistance <= targetNoiseMaker.noiseVolume + hearingDistance )
            {
                return true;
            }

        }

        // Otherwise, return false
        return false;

    }
}
