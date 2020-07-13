using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int currentHealth;
    public Stat maxHealth;
    public Stat damage;

    // Event to detect whether the charatcter dies.
    public event System.Action OnHealthReachedZero;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        // If we hit 0, Die.
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthReachedZero?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
    }

  
}
