using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankShooter))]
[RequireComponent (typeof(TankMover))]
public class TankPawn : Pawn
{
    private const float forwardDirection = 1f;
    private const float backwardDirection = -1f;
    public float forwardSpeed = 5f;
    public float backwardSpeed = 3f;
    public float tankRotationSpeed;
    public float fireForce = 1000;
    public float damageDone = 20;
    public float shellLifespan = 1.5f;
    public GameObject shellPrefab;
    private float secondsSinceLastShot = Mathf.Infinity;
    public float shotCooldownTime = 1f;
    public Noisemaker noise;
    
    // Start is called before the first frame update
    public override void Start()
    {
        mover = GetComponent<TankMover>();
        shooter = GetComponent<TankShooter>();
        noise = GetComponent<Noisemaker>();
    }

    // Update is called once per frame
    public override void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
    }

    public override void MoveForward()
    {
        mover.Move(forwardSpeed, forwardDirection);
        if (noise != null)
        {
            noise.MakeNoise(10);
        }
    }

    public override void MoveBackward()
    {
        mover.Move(backwardSpeed, backwardDirection);
        if (noise != null)
        {
            noise.MakeNoise(10);
        }
    }

    public override void Rotate(float direction)
    {//direction is set in the player controller, if the left key is pushed it's multiplied by -1 and becomes negative, if right then it's multiplied by 1 and nothing else happens
        mover.Rotate(tankRotationSpeed * direction);
        if (noise != null)
        {
            noise.MakeNoise(5);
        }
    }

    public override void Shoot()
    {//If the time that has passed since the last shot is more than the time we set between shots, we can shoot. if not, nothing happens
        if (secondsSinceLastShot > shotCooldownTime)
        {
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
            if (noise != null)
            {
                noise.MakeNoise(20);
            }
            secondsSinceLastShot = 0;
        }
        
    }

    public override void RotateTowards(Vector3 targetPosition)
    {   
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, tankRotationSpeed*Time.deltaTime);
    }

    public override void RotateAway(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(-vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, tankRotationSpeed * Time.deltaTime);
    }
}
