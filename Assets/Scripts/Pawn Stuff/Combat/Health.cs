using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float minHealth = 0f;
    public float currentHealth;
    public float maxHealth =100f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Pawn source)
    {   //make sure current health can't go below zero
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);
        Debug.Log(source.name + " SLAMS " + gameObject.name + " WITH " + damage + " POINTS OF DAMAGE!");
        //if current health is zero, Die
        if (Mathf.Approximately(currentHealth, minHealth))
        {
            Die(source);
        }
    }

    public void HealDamage(float amount)
    {   //makes sure health can't go above max
        currentHealth = Mathf.Clamp(currentHealth + amount, minHealth, maxHealth);
    }

    public void Die(Pawn source) 
    {   //when you die, you are destroyed
        Destroy(gameObject);
        Debug.Log(source.name + " DESTROYS " + gameObject.name + " WITH FACTS AND LOGIC!");
    }
}
