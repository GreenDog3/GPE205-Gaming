using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public GameObject spawnedPickup;
    public float spawnDelay;
    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {   //set inital spawn time
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedPickup == null) //if there's no pickup
        {
            if (Time.time > nextSpawnTime)
            {//spawn one and set the delay again
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else //if there is a pickup there, do nothing, set the delay again
        {
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
    private void OnDestroy()
    {
        Destroy(spawnedPickup);
    }
}
