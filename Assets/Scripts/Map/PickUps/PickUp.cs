using UnityEngine;

[RequireComponent (typeof(Collider))]
public abstract class PickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        // Set our collider to be a trigger
        Collider theCollider = GetComponent<Collider>();
        theCollider.isTrigger = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // TODO: Anything our pickup does every frame draw - spin, bounce, show a light, ect
    }

    public virtual void OnTriggerEnter( Collider other )
    {
        //
    }
}
