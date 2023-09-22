using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidfirePowerup : Powerup
{
    public float timeToSubtract;
    public float timeToReturnTo;
    public override void Apply(PowerupManager target)
    {
        TankPawn targetPawn = target.GetComponent<TankPawn>();
        if (targetPawn != null )
        {
            targetPawn.shotCooldownTime = targetPawn.shotCooldownTime - timeToSubtract;
            if (targetPawn.shotCooldownTime <= 0)
            {
                targetPawn.shotCooldownTime = 0.1f;
            }
        }
    }

    public override void Remove(PowerupManager target)
    {
        TankPawn targetPawn = target.GetComponent<TankPawn>();
        if (targetPawn != null)
        {
            targetPawn.shotCooldownTime = timeToReturnTo;
        }
    }
}
