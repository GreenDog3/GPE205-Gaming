using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private const float minHealth = 0f;
    public float currentHealth;
    public float maxHealth =100f;
    public int pointsWorth;
    public Pawn pawn;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        pawn = GetComponent<Pawn>();
    }

    private void Update()
    {
        image.fillAmount = currentHealth/maxHealth;
        if (IsBreakingContainment() == true)
        {
            Die(100, pawn);
            GameManager.instance.TryGameOver();
        }
    }

    public void TakeDamage(float damage, Pawn source)
    {   //make sure current health can't go below zero
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);
        if (source != null)
        {
            Debug.Log(source.name + " SLAMS " + gameObject.name + " WITH " + damage + " POINTS OF DAMAGE!");
            GameManager.instance.TryGameOver();
        }
        
        //if current health is zero, Die
        if (Mathf.Approximately(currentHealth, minHealth))
        {
            Die(pointsWorth, source);
            GameManager.instance.TryGameOver();
        }
    }

    public void HealDamage(float amount)
    {   //makes sure health can't go above max
        currentHealth = Mathf.Clamp(currentHealth + amount, minHealth, maxHealth);
    }

    public void Die(int points, Pawn source) 
    {   //when you die, you are destroyed
        Destroy(gameObject);
        if (source != null)
        {
            Debug.Log(source.name + " DESTROYS " + gameObject.name + " WITH FACTS AND LOGIC!");
            source.controller.AddToScore(points);
            GameManager.instance.TryGameOver();
        }
        
    }

    public bool IsBreakingContainment()
    {
        if (pawn.transform.position.y <= -10)
        {
            return true;
        }
        return false;
    }
}
