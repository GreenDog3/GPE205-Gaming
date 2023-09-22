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

    [Header("Waypoints")]
    public List<Transform> waypoints;
    public GameObject mapGeneratorPrefab;
    public int typeOfMap;
    public int mapSeed;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject slackerPrefab;
    public GameObject guardPrefab;
    public GameObject sniperPrefab;
    public GameObject leeroyPrefab;


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
        SpawnPlayer();
        SpawnSlacker();
        SpawnGuard();
        SpawnSniper();
        SpawnLeeroy();
    }

    // Update is called once per frame
    void Update()
    {
        /*
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
        } */

        if (players.Count == 0)
        {
            SpawnPlayer();
        }
    }
    public void SpawnMap()
    {
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
}
