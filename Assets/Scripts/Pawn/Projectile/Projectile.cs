using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan;
    public float damageDone;
    public Pawn parentPawn;
    private void Start()
    {
        Destroy(gameObject, lifespan);
    }
}
