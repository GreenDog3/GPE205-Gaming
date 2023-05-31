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
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);
        Debug.Log(source.name + " SLAMS " + gameObject.name + " WITH " + damage + " POINTS OF DAMAGE!");
        if (Mathf.Approximately(currentHealth, minHealth))
        {
            Die(source);
        }
    }

    public void HealDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, minHealth, maxHealth);
    }

    public void Die(Pawn source) 
    {
        Destroy(gameObject);
        Debug.Log(source.name + " DESTROYS " + gameObject.name + " WITH FACTS AND LOGIC!");
    }
}
