using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    public Transform muzzleLocation;
    
    public abstract void Shoot();
    public abstract void Shoot( float shootForce );
}
