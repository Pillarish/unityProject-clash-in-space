using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public event Action<float> onHealthPercentChange = delegate {};

    public Stat damage;
    public Stat range;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0) {
            Die();
        }

        float currentHealthPercent = (float)currentHealth / (float)maxHealth;
        onHealthPercentChange(currentHealthPercent);
    }

    public virtual void Die()
    {
        // override this
        Destroy(this.gameObject);
    }
}
