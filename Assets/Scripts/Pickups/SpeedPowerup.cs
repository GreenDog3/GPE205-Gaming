using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Powerup
{
    public float speed;
    public float defaultForwardSpeed;
    public float defaultBackwardSpeed;

    public override void Apply(PowerupManager target)
    {
        TankPawn targetPawn = target.GetComponent<TankPawn>();
        if (targetPawn != null )
        {
            //increase speed by powerup speed
            targetPawn.forwardSpeed = speed;
            targetPawn.backwardSpeed = speed;
        }
    }

    public override void Remove(PowerupManager target)
    {
        TankPawn targetPawn = target.GetComponent<TankPawn>();
        if (targetPawn != null)
        {
            //decrease speed by powerup speed
            targetPawn.forwardSpeed = defaultForwardSpeed;
            targetPawn.backwardSpeed = defaultBackwardSpeed;
        }
    }
}
