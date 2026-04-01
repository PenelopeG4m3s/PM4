using UnityEngine;

public class ShooterTank : Shooter
{
    public GameObject bulletPrefab;
    private PawnTank pawn;
    public float fireRate; // How many shots per second we can fire
    public float nextShootTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pawn = GetComponent<PawnTank>();
        nextShootTime = Time.time; // I can shoot again RIGHT NOW!
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot()
    {
        if ( Time.time >= nextShootTime )
        {
            Shoot( pawn.shootForce );
            nextShootTime = Time.time + ( 1/fireRate ); // Invert our fire rate to turn shots/second to seconds/shot
        }
        else
        {
            ///Debug.Log("A");
        }
    }

    public override void Shoot( float shootForce )
    {
        //Debug.Log("AAA");
        // instantiate the bullet at the muzzleLocation and rotation
        GameObject bulletObject = Instantiate<GameObject>( bulletPrefab, muzzleLocation.position, muzzleLocation.rotation );

        // Make the projectile remember who's its parent is
        bulletObject.GetComponent<Projectile>().parentPawn = pawn;
        bulletObject.transform.parent = pawn.gameObject.transform.parent;

        // Push it forward
        Rigidbody rb = bulletObject.GetComponent<Rigidbody>();
        rb.AddForce(muzzleLocation.forward * pawn.shootForce);
    }
}