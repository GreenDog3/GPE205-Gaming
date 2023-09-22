using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

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
    public bool is2PGame;
    public int typeOfMap;
    public int mapSeed;
    public AudioMixer mixer;
    public AudioSource sfxSource;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject player2Prefab;
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
    public TextMeshProUGUI playerTwoScore;
    public TextMeshProUGUI playerTwoLives;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameOverP1Score;
    public TextMeshProUGUI gameOverP2Score;

    [Header("Player Info")]
    public GameObject playerOne;
    public GameObject playerTwo;
    public int p1Score;
    public int p2Score;
    public int p1Lives;
    public int p2Lives;
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
        GameManager.instance.DisplayP2Lives();
        GameManager.instance.DisplayP2Score();
        if (currentMapGenerator != null)
        {
            if (p1Lives > 0)
            {
                if (playerOne == null)
                {
                    SpawnPlayer();
                }


            }

            if (is2PGame == true)
            {
                if (p2Lives > 0)
                {
                    if (playerTwo == null)
                    {
                        SpawnPlayerTwo();
                    }

                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            DestroyEnemies();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            TryGameOver();
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
        playerOne = newPawnObj;
    }
    public void SpawnPlayerTwo()
    {
        PlayerSpawner[] playerSpawns = FindObjectsOfType<PlayerSpawner>();
        int randomIndex = Random.Range(0, playerSpawns.Length);

        GameObject newPawnObj = Instantiate(player2Prefab, playerSpawns[randomIndex].transform.position, playerSpawns[randomIndex].transform.rotation);
        playerTwo = newPawnObj;
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
        if (is2PGame == true)
        {
            p2Lives = startingLives;
        }
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
            gameOverP1Score.text = "P1 Score: " + p1Score;
            if (is2PGame == true)
            {
                gameOverP2Score.text = "P2 Score: " + p2Score;
            }
            else
            {
                gameOverP2Score.text = " ";
            }
            if (currentMapGenerator != null)
            {
                currentMapGenerator.ResetLevel();
                DestroyTanks();
            }
        }
        else
        {
            gameOverText.text = "You lose...";
            gameOverP1Score.text = "P1 Score: " + p1Score;
            if (is2PGame == true)
            {
                gameOverP2Score.text = "P2 Score: " + p2Score;
            }
            else
            {
                gameOverP2Score.text = " ";
            }
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

    public void DisplayP2Score()
    {
        if (is2PGame == true)
        {
            playerTwoScore.text = "P2 Score: " + p2Score;
        }
        else
        {
            playerTwoScore.text = " ";
        }
        
    }

    public void DisplayP2Lives()
    {
        if (is2PGame == true)
        {
            playerTwoLives.text = "P2 Lives: " + p2Lives;
        }
        else
        {
            playerTwoLives.text = " ";
        }
    }

    public void TryGameOver()
    {
        if (is2PGame == false)
        {
            Debug.Log("Game Over Attempted at " + Time.time);
            bool isGameOver = true;

            //default true
            if ((p1Lives > 0))
            {
                isGameOver = false;
            }
            else if (enemies.Count > 0)
            {
                isGameOver = false;
            }
            else
            {
                isGameOver = true;
            }
            if (enemies.Count == 0)
            {
                isGameOver = true;
            }
            if (players.Count == 0)
            {
                isGameOver = true;
            }



            if (isGameOver == true)
            {
                if (players.Count == 0)
                {
                    //If there are no players, the players lose
                    ActivateGameOverState(false);

                }
                if (enemies.Count == 0) //if there are players
                {
                    //if there are no more enemies, the game is over and we win
                    ActivateGameOverState(true);
                }
            }
        }
        else
        {
            Debug.Log("Game Over Attempted at " + Time.time);
            bool isGameOver = true;

            //default true
            if ((p1Lives > 0))
            {
                isGameOver = false;
            }
            //p1 is dead, this would be true
            if (p2Lives > 0)
            {
                isGameOver = false;
            }
            //p2 is alive, therefore it is false
            else
            {
                isGameOver = true;
            }

            if (enemies.Count == 0)
            {
                isGameOver = true;
            }
            if (players.Count == 0)
            {
                isGameOver = true;
            }

            if (isGameOver == true)
            {
                if (players.Count == 0)
                {
                    //If there are no players, the players lose
                    ActivateGameOverState(false);

                }
                if (enemies.Count == 0) //if there are players
                {
                    //if there are no more enemies, the game is over and we win
                    ActivateGameOverState(true);
                }
            }
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
