using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;

    public void OnTriggerEnter(Collider other)
    {
        //get the powerup manager of the incoming object
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();
        //if the object has a powerup manager
        if (powerupManager != null )
        {
            //give it the powerup and destroy the pickup
            powerupManager.Add(powerup);
            Destroy(gameObject);
        }
        
    }
}
