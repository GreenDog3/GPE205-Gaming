using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int points = 0;
    [Header("Tank Lists")]
    public List<Controller> players;
    public List<Controller> enemies;

    [Header("Map Settings")]
    public GameObject mapGeneratorPrefab;
    public MapGenerator currentMapGenerator;
    public int typeOfMap;
    public int mapSeed;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject slackerPrefab;
    public GameObject guardPrefab;
    public GameObject sniperPrefab;
    public GameObject leeroyPrefab;

    [Header("UI Pages")]
    public GameObject titleObject;
    public GameObject mainMenuObject;
    public GameObject creditsObject;
    public GameObject optionsObject;
    public GameObject gameplayObject;
    public GameObject gameOverObject;
    public TextMeshProUGUI playerOneScore;
    public TextMeshProUGUI playerOneLives;
    public TextMeshProUGUI gameOverText;

    [Header("Player Info")]
    public int p1Score;
    public int p1Lives;
    public int startingLives;


    private void Awake()
    {
        if (instance == null)
        {   //If there's no gamemanager, cool! make one
            instance = this;
        }
        else
        {   //If there already is a gamemanager, bad! destroy yourself as quickly as possible to not screw up the timeline.
            Debug.LogWarning("Attempted to create Game Manager 2: 2Game 2Manager. Terminating before it becomes a soulless Hollywood franchise");
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject); //makes sure the game manager persists between scenes
    }
    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllStates();
        ActivateTitleState();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.DisplayP1Lives();
        GameManager.instance.DisplayP1Score();
        if (currentMapGenerator != null)
        {
            if (p1Lives > 0)
            {
                if (players.Count == 0)
                {
                    SpawnPlayer();
                }

            }
            else
            {
                ActivateGameOverState(false);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                DestroyEnemies();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                TryGameOver();
            }



        }


    }
    public void SpawnMap()
    {
        if (currentMapGenerator != null)
        {
            currentMapGenerator.ResetLevel();
        }
        
        GameObject newMapGenerator = Instantiate(mapGeneratorPrefab, Vector3.zero, Quaternion.identity);
    }
    public void SpawnPlayer()
    {
        PlayerSpawner[] playerSpawns = FindObjectsOfType<PlayerSpawner>();
        int randomIndex = Random.Range(0, playerSpawns.Length);

        GameObject newPawnObj = Instantiate(playerPrefab, playerSpawns[randomIndex].transform.position, playerSpawns[randomIndex].transform.rotation);
    }

    public void SpawnSlacker()
    {
        EnemySpawner[] enemySpawns = FindObjectsOfType<EnemySpawner>();
        int randomIndex = Random.Range(0, enemySpawns.Length);
        GameObject newSlackerObj = Instantiate(slackerPrefab, enemySpawns[randomIndex].transform.position, enemySpawns[randomIndex].transform.rotation);
    }

    public void SpawnGuard()
    {
        EnemySpawner[] enemySpawns = FindObjectsOfType<EnemySpawner>();
        int randomIndex = Random.Range(0, enemySpawns.Length);
        GameObject newGuardObj = Instantiate(guardPrefab, enemySpawns[randomIndex].transform.position, enemySpawns[randomIndex].transform.rotation);
        newGuardObj.GetComponent<GuardAIController>().waypoints = enemySpawns[randomIndex].waypoints;
        
    }

    public void SpawnSniper()
    {
        EnemySpawner[] enemySpawns = FindObjectsOfType<EnemySpawner>();
        int randomIndex = Random.Range(0, enemySpawns.Length);
        GameObject newSniperObj = Instantiate(sniperPrefab, enemySpawns[randomIndex].transform.position, enemySpawns[randomIndex].transform.rotation);
        newSniperObj.GetComponent<SniperAIController>().homeBase = enemySpawns[randomIndex].homeBase;
    }

    public void SpawnLeeroy()
    {
        EnemySpawner[] enemySpawns = FindObjectsOfType<EnemySpawner>();
        int randomIndex = Random.Range(0, enemySpawns.Length);
        GameObject newLeeroyObj = Instantiate(leeroyPrefab, enemySpawns[randomIndex].transform.position, enemySpawns[randomIndex].transform.rotation);
    }

    public void DeactivateAllStates()
    {
        if (titleObject != null)
        {
            titleObject.SetActive(false);
        }
        if (mainMenuObject != null)
        {
            mainMenuObject.SetActive(false);
        }
        if (optionsObject != null)
        {
            optionsObject.SetActive(false);
        }
        if (creditsObject != null)
        {
            creditsObject.SetActive(false);
        }
        if (gameplayObject != null)
        {
            gameplayObject.SetActive(false);
        }
        if (gameOverObject != null)
        {
            gameOverObject.SetActive(false);
        }
        
    }

    public void ActivateTitleState()
    {
        DeactivateAllStates();
        titleObject.SetActive(true);
    }

    public void ActivateMainMenuState()
    {
        DeactivateAllStates();
        mainMenuObject.SetActive(true);
    }

    public void ActivateOptionsState()
    {
        DeactivateAllStates();
        optionsObject.SetActive(true);
    }

    public void ActivateCreditsState()
    {
        DeactivateAllStates();
        creditsObject.SetActive(true);
    }
    public void ActivateGameplayState()
    {
        p1Lives = startingLives;
        //Deactivate other states
        DeactivateAllStates();
        //Reset level
        if (currentMapGenerator != null)
        {
            currentMapGenerator.ResetLevel();
            DestroyTanks();
            
        }
        //spawn map
        SpawnMap();
        //spawn tanks
        SpawnGuard();
        SpawnSlacker();
        SpawnSniper();
        SpawnLeeroy();
        //activate gameplay screen to start game
        gameplayObject.SetActive(true);
    }

    public void ActivateGameOverState(bool victory)
    {
        DeactivateAllStates();
        if (gameOverObject != null)
        {
            gameOverObject.SetActive(true);
            
            
        }
        if (victory == true)
        {
            gameOverText.text = "You win!";
            if (currentMapGenerator != null)
            {
                currentMapGenerator.ResetLevel();
                DestroyTanks();
            }
        }
        else
        {
            gameOverText.text = "You lose...";
            if (currentMapGenerator != null)
            {
                currentMapGenerator.ResetLevel();
                DestroyTanks();
            }
        }

    }

    public void DisplayP1Score()
    {
        playerOneScore.text = "P1 Score: " + p1Score;
    }

    public void DisplayP1Lives()
    {
        playerOneLives.text = "P1 Lives: " + p1Lives;
    }

    public void TryGameOver()
    {
        Debug.Log("Game Over Attempted at " + Time.time);
        bool isGameOver = true;
        
        //default true
        if (p1Lives > 0)
        {
            isGameOver = false;
        }

        if (isGameOver == true)
        {
            ActivateGameOverState(false);
        }

        if (enemies.Count == 0) //if we don't have 0 lives
        {
            //if there are no more enemies, the game is over and we win
            isGameOver = true;
        }
        if (isGameOver == true)
        {
            ActivateGameOverState(true);
            
        }
    }

    public void DestroyTanks()
    {
        //clear tanks
        for (int i = 0; i < players.Count; i++)
        {
            Destroy(players[i].pawn.gameObject);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].pawn.gameObject);
        }
    }

    public void DestroyEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].pawn.gameObject);
        }
    }
}
