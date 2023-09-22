using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups;
    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimers();
    }

    public void Add(Powerup powerupToAdd)
    {
        //applies the powerup to the tank
        powerupToAdd.Apply(this);
        //add it to the list of powerups
        powerups.Add(powerupToAdd);
    }

    public void Remove(Powerup powerupToRemove)
    {
        //removes it from the tank
        powerupToRemove.Remove(this);
        //removes it from the list
        powerups.Remove(powerupToRemove);

    }

    public void DecrementPowerupTimers()
    {
        List<Powerup> removedPowerupQueue = new List<Powerup>();
        foreach (Powerup pu in powerups)
        {
            //time goes down
            pu.duration -= Time.deltaTime;

            //when the time is up
            if(pu.duration <= 0)
            {
                removedPowerupQueue.Add(pu);
            }
        }
        //remove the powerups that have run out of duration
        foreach (Powerup powerup in removedPowerupQueue)
        {
            Remove(powerup);
        }
    }
}