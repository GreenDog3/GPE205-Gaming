using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int points = 0;
    [Header("Tank Lists")]
    public List<Controller> players;
    public List<Controller> enemies;
    public List<Transform> waypoints;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject slackerPrefab;
    public GameObject guardPrefab;
    public GameObject sniperPrefab;
    public GameObject leeroyPrefab;

    [Header("Spawnpoints")]
    public Transform playerSpawn;
    public Transform enemySpawn;


    private void Awake()
    {
        if (instance == null)
        {   //If there's no gamemanager, cool! make one
            instance = this;
        }
        else
        {   //If there already is a gamemanager, bad! destroy yourself as quickly as possible to not screw up the timeline.
            Debug.LogWarning("Attempted to create Game Manager 2: 2Game 2Manager");
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject); //makes sure the game manager persists between scenes
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnSlacker();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
            SpawnGuard(); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnSniper();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnLeeroy();
        }
    }

    public void SpawnPlayer()
    {
        GameObject newPawnObj = Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
    }

    public void SpawnSlacker()
    {
        GameObject newSlackerObj = Instantiate(slackerPrefab, enemySpawn.transform.position, enemySpawn.transform.rotation);
    }

    public void SpawnGuard()
    {
        GameObject newGuardObj = Instantiate(guardPrefab, enemySpawn.transform.position, enemySpawn.transform.rotation);
        newGuardObj.GetComponent<GuardAIController>().waypoints = waypoints;
        
    }

    public void SpawnSniper()
    {
        GameObject newSniperObj = Instantiate(sniperPrefab, enemySpawn.transform.position, enemySpawn.transform.rotation);
    }

    public void SpawnLeeroy()
    {
        GameObject newLeeroyObj = Instantiate(leeroyPrefab, enemySpawn.transform.position, enemySpawn.transform.rotation);
    }
}
