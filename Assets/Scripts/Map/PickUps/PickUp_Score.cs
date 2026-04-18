using UnityEngine;

public class PickUp_Score : PickUp
{
    private AudioManage audioManage;
    public PowerUp_Score powerup;
    public AudioClip pickUpSound;

    public override void Start()
    {
        base.Start();
        audioManage = GetComponent<AudioManage>();
    }

    public override void OnTriggerEnter( Collider other )
    {
        // TODO: Check the other object has a PowerUpManager
        PowerUpManager otherManager = other.GetComponent<PowerUpManager>();

        if ( otherManager != null )
        {
            // Add powerup
            otherManager.Add(powerup);

            // Destroy this object
            Destroy(gameObject);
        }

        base.OnTriggerEnter(other);

        audioManage.PlayAudio( pickUpSound );
    }
}
