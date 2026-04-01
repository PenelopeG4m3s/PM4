using UnityEngine;
[RequireComponent(typeof(Collider))]

public class DamageOtherOnOverlap : MonoBehaviour
{
    public float damageDone;
    private Collider _collider;
    private Projectile _projectile;

    public void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _projectile = GetComponent<Projectile>();
        if (_projectile != null)
        {
            damageDone = _projectile.damageDone;
        }
    }

    public void OnTriggerEnter( Collider other )
    {
        // Get the other objects health component
        Health otherHealth = other.GetComponent<Health>();
        // If it has one:
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageDone);
        }

        // Destroy this projectile
        Destroy(gameObject);
    }
}
