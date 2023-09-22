using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damage =20f;
    public Pawn owner;
    public AudioClip clip;
    public AudioSource source;

    private void Start()
    {
        source = GameManager.instance.sfxSource;
    }
    private void OnCollisionEnter(Collision other)
    {
        //get health of other gameobject
        Health otherHealth = other.gameObject.GetComponent<Health>();
        //only do damage if damage can be done
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage, owner);
            source.PlayOneShot(clip);
        }

        //destroy projectile
        Destroy(gameObject);
    }
}