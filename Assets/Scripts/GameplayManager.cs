using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    public GameObject spawnerPrefab;
    public GameObject playerUIPrefab;
    public GameObject playerCameraPrefab;
    [Header("GameInProgress")]
    public bool GameInProgress;
    [Header("MapGenerator")]
    public MapGenerator mapGenerator;
    [Header("List")]
    public List <GameObject> pickups;
    public List <GameObject> myChildren;
    [Header("Audio")]
    public AudioSource audioSource;

    // Call this function to begin gameplay
    public void StartGame( int playerCount, RandomType randomType )
    {
        StartBackgroundMusic();
        // Check if the game has already been started so that we don't start the game again
        if ( !GameInProgress )
        {
            // Generate the map
            mapGenerator.InitializeRandom();
            mapGenerator.GenerateMap();

            // Reset the scores
            GameManager.instance.scores = new int[playerCount];

            // Spawn the player
            //Debug.Log("SpawnPointCount" + GameManager.instance.playerSpawnPoints.Count);
            for ( int i = 0; i < playerCount; i++)
            {
                SpawnPlayer( i + 1, playerCount );
            }
            //SpawnPlayer();

            // Spawn the enemies
            GameManager.instance.SpawnEnemy( (int)1, "Soldier" );
            GameManager.instance.SpawnEnemy( (int)1, "Patroller");
            GameManager.instance.SpawnEnemy( (int)1, "Runner" );
            GameManager.instance.SpawnEnemy( (int)1, "Vulture" ); 

            // Spawn the spawners
            SpawnItemSpawner( GameManager.instance.itemSpawnPoints.Count );

            // Set the game to be in progress
            GameInProgress = true;
        }
    }

    public void LateUpdate()
    {
        if (GameInProgress)
        {
            // TODO: check if there are any enemies
            if (GameManager.instance.enemies.Count == 0)
            {
                GameInProgress = false;
                WinGame();
            }
            // TODO: check if there are any players
            if (CheckPlayers())
            {
                GameInProgress = false;
                LoseGame();
            }
        }
    }

    public bool CheckPlayers()
    {
        foreach (ControllerPlayer player in GameManager.instance.players)
        {
            if (player.currentLives > 0)
            {
                return false;
            }
        }
        return true;
    }

    // Call this function to end the gameplay
    public void ResetGame()
    {
        // TODO: Destroy self to destroy all child objects
        List<GameObject> objectsToDestroy = new List<GameObject>();
        foreach ( GameObject gameObject in myChildren)
        {
            objectsToDestroy.Add(gameObject);
        }
        foreach ( GameObject gameObject in objectsToDestroy )
        {
            Destroy(gameObject);
            myChildren.Remove(gameObject);
        }
        StopBackgroundMusic();
    }

    // Spawn everything
    #region

    public void SpawnPlayer( int playerNumber, int playerCount )
    {
        // Choose a spawnpoint from the list
        Transform playerSpawn = GameManager.instance.playerSpawnPoints[Random.Range(0,GameManager.instance.playerSpawnPoints.Count)].transform;
        
        // Spawn a tank pawn (and store it in tanks)
        Pawn tempTankPawn = SpawnTank( playerPawnPrefab, Vector3.zero );

        // Set the camera
        //PlayerCamera playerCamera = tempTankPawn.gameObject.GetComponent<PlayerCamera>();
        //playerCamera.SetCamera( playerNumber, playerCount );

        // Instantiate the camera
        PlayerCamera tempPlayerCamera = SpawnPlayerCamera( playerCameraPrefab );
        tempPlayerCamera.SetCamera( playerNumber, playerCount );
        tempPlayerCamera.SetPawn( tempTankPawn );

        // Spawn a player controller (and store it in player)
        ControllerPlayer tempPlayerController = SpawnPlayerController(playerControllerPrefab, playerNumber);

        // Set the cam variable for the controller
        tempPlayerController.myCamera = tempPlayerCamera;

        // Have the player possess the pawn
        tempPlayerController.Possess(tempTankPawn);

        // Move to spawnpoint
        tempTankPawn.transform.position = playerSpawn.position;

        // Add Audio Listener to player tank
        //tempTankPawn.gameObject.AddComponent<AudioListener>();

        // Spawn player UI
        SpawnPlayerUI( playerUIPrefab, tempPlayerController, tempPlayerCamera.gameObject.GetComponent<Camera>() );
    }

    public Pawn SpawnTank( GameObject prefab, Vector3 position )
    {
        GameObject tempTankObject = Instantiate<GameObject>(prefab, position, Quaternion.identity);
        GameManager.instance.SetParent( tempTankObject );
        return tempTankObject.GetComponent<Pawn>();
    }

    public ControllerPlayer SpawnPlayerController ( GameObject prefab, int playerNumber )
    {
        GameObject tempPlayer = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity );
        tempPlayer.GetComponent<ControllerPlayer>().playerNumber = playerNumber;
        // Tell the player controller what number it is
        tempPlayer.name = "PlayerController" + playerNumber;
        GameManager.instance.SetParent( tempPlayer );
        return tempPlayer.GetComponent<ControllerPlayer>();
    }

    public PlayerCamera SpawnPlayerCamera ( GameObject prefab )
    {
        GameObject tempPlayerCamera = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity );
        GameManager.instance.SetParent( tempPlayerCamera );
        return tempPlayerCamera.GetComponent<PlayerCamera>();
    }

    public void SpawnPlayerUI( GameObject prefab, ControllerPlayer player, Camera camera )
    {
        GameObject playerUI = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity);
        playerUI.GetComponent<Canvas>().worldCamera = camera;
        playerUI.GetComponent<PlayerUI>().player = player;
        GameManager.instance.SetParent( playerUI );
        playerUI.GetComponent<Canvas>().planeDistance = 1;
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
            GameManager.instance.SetParent(tempSpawner);

            // Put the spawner in the pickupSpawn position
            tempSpawner.transform.position = pickupSpawn.position;
        }
    }

    #endregion

    //public void SetParent( GameObject childObject )
    //{
        //childObject.transform.parent = transform;
        //myChildren.Add( childObject );
    //}

    public void WinGame()
    {
        ResetGame();
        GameManager.instance.ActivateWinScreen();
    }

    public void LoseGame()
    {
        ResetGame();
        GameManager.instance.ActivateGameOverScreen();
    }
    
    public void StartBackgroundMusic()
    {
        audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        audioSource.Stop();
    }
}