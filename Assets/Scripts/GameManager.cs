using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public static GameManager instance;
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    public GameObject enemyControllerSoldierPrefab;
    public GameObject enemyControllerPatrollerPrefab;
    public GameObject enemyControllerRunnerPrefab;
    public GameObject enemyControllerVulturePrefab;
    public GameObject enemyPawnPrefab;
    [Header("Up-to-date Lists")]
    public List <Pawn> tanks;
    public List <ControllerPlayer> players;
    public List <ControllerAI> enemies;
    public List <PlayerSpawn> playerSpawnPoints;
    public List <EnemySpawn> enemySpawnPoints;
    public List <ItemSpawn> itemSpawnPoints;
    //public List <float> scores;
    public int[] scores;
    [Header("GameStates")]
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject StartGameScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;
    public GameObject WinScreenStateObject;
    [Header("Game Settings")]
    // Add some sort of variable for whether you use random map or map of day
    public float masterVolume = 20.0f;
    public float musicVolume = 20.0f;
    public float sfxVolume = 20.0f;
    [Header("AudioStuff")]
    public AudioSource audioSource;

    void Awake()
    {
        // Create our singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        // Create our up to date list objects (not just memory locations )
        tanks = new List<Pawn>();
        players = new List<ControllerPlayer>();
        enemies = new List<ControllerAI>();
        scores = new int[0];
    }

    void Start()
    {
        ActivateTitleScreen();
    }


    // Consider moving all of this to a separate script that the gameplay object will have?
    // Then the gamemanager will reference it since the gameplay object will have better control over its own objects
    // Which would then allow for the gameplay object to kill all of its children easier
    #region
    //public void StartGame()
    //{
        // Do everything that we need to start the game

        // Spawn the player
        //SpawnPlayer();
        // Spawn enemies
        //SpawnEnemies();

    //}

    //public void SpawnPlayer()
    //{
        // Spawn a tank pawn (and store it in tanks)
        //Pawn tempTankPawn = SpawnTank( playerPawnPrefab, Vector3.zero );

        // Spawn a player controller (and store it in player)
        //Controller tempPlayerController = SpawnPlayerController(playerControllerPrefab);

        // Have the player possess the pawn
        //tempPlayerController.Possess(tempTankPawn);
    //}

    //public void SpawnEnemies()
    //{
        // Spawn a tank (and store it in tanks)
        //Pawn tempTankPawn1 = SpawnTank( enemyPawnPrefab, new Vector3( 5.0f, 0.0f, -5.0f ) );
        //Pawn tempTankPawn2 = SpawnTank( enemyPawnPrefab, new Vector3( 5.0f, 0.0f, 5.0f ) );
        //Pawn tempTankPawn3 = SpawnTank( enemyPawnPrefab, new Vector3( -5.0f, 0.0f, 5.0f ) );
        //Pawn tempTankPawn4 = SpawnTank( enemyPawnPrefab, new Vector3( -5.0f, 0.0f, -5.0f ) );

        // Spawn an enemy controller ( and store it in enemies)
        //ControllerAI_Soldier tempTankController1 = SpawnEnemySoldierController( enemyControllerSoldierPrefab );
        //ControllerAI_Patroller tempTankController2 = SpawnEnemyPatrollerController( enemyControllerPatrollerPrefab );
        //ControllerAI_Runner tempTankController3 = SpawnEnemyRunnerController( enemyControllerRunnerPrefab );
        //ControllerAI_Vulture tempTankController4 = SpawnEnemyVultureController( enemyControllerVulturePrefab );

        // Have the enemy possess the pawn
        //tempTankController1.Possess( tempTankPawn1 );
        //tempTankController2.Possess( tempTankPawn2 );
        //tempTankController3.Possess( tempTankPawn3 );
        //tempTankController4.Possess( tempTankPawn4 );
    //}

    public Pawn SpawnTank( GameObject prefab, Vector3 position )
    {
        GameObject tempTankObject = Instantiate<GameObject>(prefab, position, Quaternion.identity);
        return tempTankObject.GetComponent<Pawn>();
    }

    //public Controller SpawnPlayerController ( GameObject prefab )
    //{
        //GameObject tempPlayer = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity );
        //return tempPlayer.GetComponent<Controller>();
    //}

    public void SpawnEnemy( int spawnAmount, string enemyType )
    {
        // Create every enemy we want to spawn
        for ( int i = 0; i < spawnAmount; i++ )
        {
            // Grab the intended spawnpoint
            // Choose a spawnpoint from the list
            Transform enemySpawn = enemySpawnPoints[Random.Range(0,enemySpawnPoints.Count)].transform;

            // Create the tank
            Pawn tempTankPawn = SpawnTank( enemyPawnPrefab, Vector3.zero );

            // Set the pawn to be the child of the gameplay state
            tempTankPawn.gameObject.transform.parent = GameplayStateObject.transform;
            GameplayStateObject.GetComponent<GameplayManager>().myChildren.Add(tempTankPawn.gameObject);

            // Check what type of controller we want
            switch (enemyType)
            {
                // Soldier
                case "Soldier":
                    // Create the controller
                    ControllerAI_Soldier tempSoldierController = SpawnEnemySoldierController( enemyControllerSoldierPrefab );

                    // Tell the controller to possess the tank
                    tempSoldierController.Possess( tempTankPawn );

                    // Set the controller to be the child of the gameplay state
                    tempSoldierController.gameObject.transform.parent = GameplayStateObject.transform;
                    GameplayStateObject.GetComponent<GameplayManager>().myChildren.Add(tempSoldierController.gameObject);
                break;
                // Patroller
                case "Patroller":
                    // Create the controller
                    ControllerAI_Patroller tempPatrollerController = SpawnEnemyPatrollerController( enemyControllerPatrollerPrefab );

                    // Tell the controller to possess the tank
                    tempPatrollerController.Possess( tempTankPawn );

                    // Set the controller to be the child of the gameplay state
                    tempPatrollerController.gameObject.transform.parent = GameplayStateObject.transform;
                    GameplayStateObject.GetComponent<GameplayManager>().myChildren.Add(tempPatrollerController.gameObject);
                break;
                // Runner
                case "Runner":
                    // Create the controller
                    ControllerAI_Runner tempRunnerController = SpawnEnemyRunnerController( enemyControllerRunnerPrefab );

                    // Tell the controller to possess the tank
                    tempRunnerController.Possess( tempTankPawn );

                    // Set the controller to be the child of the gameplay state
                    tempRunnerController.gameObject.transform.parent = GameplayStateObject.transform;
                    GameplayStateObject.GetComponent<GameplayManager>().myChildren.Add(tempRunnerController.gameObject);
                break;
                // Vulture
                case "Vulture":
                    // Create the controller
                    ControllerAI_Vulture tempVultureController = SpawnEnemyVultureController( enemyControllerVulturePrefab );

                    // Tell the controller to possess the tank
                    tempVultureController.Possess( tempTankPawn );

                    // Set the controller to be the child of the gameplay state
                    tempVultureController.gameObject.transform.parent = GameplayStateObject.transform;
                    GameplayStateObject.GetComponent<GameplayManager>().myChildren.Add(tempVultureController.gameObject);
                    
                break;
            }

            // Move to spawnpoint
            tempTankPawn.transform.position = enemySpawn.position;
        }
    }

    public ControllerAI_Soldier SpawnEnemySoldierController ( GameObject prefab )
    {
        // TODO: Grab a random spawn for it to do
        GameObject tempPlayer = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity );
        return tempPlayer.GetComponent<ControllerAI_Soldier>();
    }

    public ControllerAI_Patroller SpawnEnemyPatrollerController ( GameObject prefab )
    {
        GameObject tempPlayer = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity );
        return tempPlayer.GetComponent<ControllerAI_Patroller>();
    }

    public ControllerAI_Runner SpawnEnemyRunnerController ( GameObject prefab )
    {
        GameObject tempPlayer = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity );
        return tempPlayer.GetComponent<ControllerAI_Runner>();
    }

    public ControllerAI_Vulture SpawnEnemyVultureController ( GameObject prefab )
    {
        GameObject tempPlayer = Instantiate<GameObject>( prefab, Vector3.zero, Quaternion.identity );
        return tempPlayer.GetComponent<ControllerAI_Vulture>();
    }

    #endregion

    // All of the game state menu stuff
    #region

    public void ActivateTitleScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        TitleScreenStateObject.SetActive(true);
        // Do whatever needs to be done when the title screen starts.
        PlayMenuMusic();
    }

    public void ActivateMainMenuScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        MainMenuStateObject.SetActive(true);
    }

    public void ActivateOptionsScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        OptionsScreenStateObject.SetActive(true);
    }

    public void ActivateCreditsScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        CreditsScreenStateObject.SetActive(true);
    }

    public void ActivateGameplayScreen( int playerCount, RandomType randomType, int seed )
    {
        // Deactivate all states
        DeactivateAllStates();
        StopMenuMusic();
        Debug.Log("IS IT STOPPED: "+audioSource.isPlaying);
        // Activate the title screen
        GameplayStateObject.SetActive(true);
        
        // Start the game by calling the gameplay managers start game
        GameplayManager gameplayManager = GameplayStateObject.GetComponent<GameplayManager>();
        gameplayManager.StartGame( playerCount, randomType, seed );
    }

    public void ActivateStartGameScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        StartGameScreenStateObject.SetActive(true);
    }

    public void ActivateGameOverScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        GameOverScreenStateObject.SetActive(true);
        PlayMenuMusic();
    }

    public void ActivateWinScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        WinScreenStateObject.SetActive(true);
        PlayMenuMusic();
    }

    private void DeactivateAllStates()
    {
        // Deactivate all Game States
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        StartGameScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
        WinScreenStateObject.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log(audioSource.isPlaying());
    }

    #endregion

    public void PlayMenuMusic()
    {
        audioSource.Play();
    }

    public void StopMenuMusic()
    {
        audioSource.Stop();
    }

    public void SetParent( GameObject childObject )
    {
        childObject.transform.parent = GameplayStateObject.transform;
        GameplayStateObject.GetComponent<GameplayManager>().myChildren.Add( childObject );
    }

}
