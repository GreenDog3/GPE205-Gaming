using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firePoint;

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        //spawn in bullet
        GameObject newShell = Instantiate(shellPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        //give bullet data
        DamageOnHit damageOnHit = newShell.GetComponent<DamageOnHit>();
        if (damageOnHit)
        {
            damageOnHit.damage = damageDone;
            damageOnHit.owner = GetComponent<Pawn>();
        }
        Rigidbody rb = newShell.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.AddForce(firePoint.transform.forward * fireForce);
        }
        Destroy(newShell, lifespan);

    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }
}
