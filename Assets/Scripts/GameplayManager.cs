using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    public GameObject spawnerPrefab;
    [Header("GameInProgress")]
    public bool GameInProgress;
    [Header("MapGenerator")]
    public MapGenerator mapGenerator;
    [Header("List")]
    public List <GameObject> pickups;

    // Call this function to begin gameplay
    public void StartGame()
    {
        // Check if the game has already been started so that we don't start the game again
        if ( !GameInProgress )
        {
            // Generate the map
            mapGenerator.InitializeRandom();
            mapGenerator.GenerateMap();

            // Spawn the player
            Debug.Log("SpawnPointCount" + GameManager.instance.playerSpawnPoints.Count);
            SpawnPlayer();

            // Spawn the enemies
            GameManager.instance.SpawnEnemy( (int)1, "Soldier" );
            //GameManager.instance.SpawnEnemy( (int)1, "Patroller");
            //GameManager.instance.SpawnEnemy( (int)1, "Runner" );
            //GameManager.instance.SpawnEnemy( (int)1, "Vulture" ); 

            // Spawn the spawners
            SpawnItemSpawner( GameManager.instance.itemSpawnPoints.Count );

            // Set the game to be in progress
            GameInProgress = true;
        }
    }

    // Call this function to end the gameplay
    public void ResetGame()
    {
        // TODO: Destroy self to destroy all child objects
    }

    public void SpawnPlayer()
    {
        // Choose a spawnpoint from the list
        Transform playerSpawn = GameManager.instance.playerSpawnPoints[Random.Range(0,GameManager.instance.playerSpawnPoints.Count)].transform;
        
        // Spawn a tank pawn (and store it in tanks)
        Pawn tempTankPawn = SpawnTank( playerPawnPrefab, Vector3.zero );

        // Spawn a player controller (and store it in player)
        Controller tempPlayerController = SpawnPlayerController(playerControllerPrefab);

        // Set the objects to be the children of the gameplay state
        tempTankPawn.gameObject.transform.parent = gameObject.transform;
        tempPlayerController.gameObject.transform.parent = gameObject.transform;

        // Have the player possess the pawn
        tempPlayerController.Possess(tempTankPawn);

        // Move to spawnpoint
        tempTankPawn.transform.position = playerSpawn.position;
    }

    public Pawn SpawnTank( GameObject prefab, Vector3 position )
    {
        GameObject tempTankObject = Instantiate<GameObject>(prefab, position, Quaternion.identity);
        return tempTankObject.GetComponent<Pawn>();
    }

    public Controller SpawnPlayerController ( GameObject prefab )
    {
        GameObject tempPlayer = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity );
        return tempPlayer.GetComponent<Controller>();
    }

    public void SpawnItemSpawner( int amountToSpawn )
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            // Grab which pickup we want the spawner to spawn
            GameObject pickup = pickups[Random.Range(0,pickups.Count)];
            
            // Grab where we want the spawner to spawn
            Transform pickupSpawn = GameManager.instance.itemSpawnPoints[Random.Range(0,GameManager.instance.itemSpawnPoints.Count)].transform;

            // Spawn the spawner
            GameObject tempSpawner = Instantiate<GameObject>( spawnerPrefab, Vector3.zero, Quaternion.identity );

            // Make the spawner spawn the pickup
            tempSpawner.GetComponent<SpawnerTimed>().objectToSpawn = pickup;

            // Set the spawner parent to this object
            tempSpawner.transform.parent = gameObject.transform;

            // Put the spawner in the pickupSpawn position
            tempSpawner.transform.position = pickupSpawn.position;
        }
    }
}