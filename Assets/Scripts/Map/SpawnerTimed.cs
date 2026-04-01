using UnityEngine;

public class SpawnerTimed : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float timeBetweenSpawns;
    public bool isSpawnOnStart;
    private float countdownTimer;
    private GameObject spawnedObject;
    public Transform point;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set initial spawn time
        if (isSpawnOnStart)
        {
            countdownTimer = 0;
        }
        else 
        {
            countdownTimer = timeBetweenSpawns;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the object exists
        if ( spawnedObject == null )
        {
            // Subtract how much time has passed
            countdownTimer -= Time.deltaTime;

            // Check if our timer hit 0
            if (countdownTimer <= 0)
            {
                // Spawn Object
                spawnedObject = Instantiate(objectToSpawn, point.position, transform.rotation) as GameObject;
                // Set object parent to me
                spawnedObject.transform.parent = gameObject.transform;
                // Reset time
                countdownTimer = timeBetweenSpawns;
            }
        }
    }
}
