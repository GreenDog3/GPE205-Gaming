using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
    public float health;

    public override void Apply(PowerupManager target)
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.HealDamage(health);
        }
    }

    public override void Remove(PowerupManager target)
    {
        //doesn't get removed since it just replenishes health
    }
}
