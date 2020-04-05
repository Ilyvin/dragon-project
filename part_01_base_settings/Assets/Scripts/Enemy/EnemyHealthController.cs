using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float maxHealth = 100f;
    public int expaValue = 10;
    public float currentHealth;
    public event Action<float> OnHealthPctChanged = delegate { };
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void changeHealth(float healthDelta, PlayerController player)
    {
        float resultHealth = currentHealth + healthDelta;
        
        if (resultHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Enemy is dead");
            player.expaController.updateExpa(expaValue);
            Debug.Log("Player got Expa: " + expaValue);
            
            OnHealthPctChanged(0f);
            Destroy(gameObject);
        }
        else if (resultHealth > maxHealth)
        {
            currentHealth = maxHealth;
            OnHealthPctChanged(100f);
        }
        else
        {
            currentHealth = resultHealth;
            float currentHealthPct = (float) currentHealth / (float) maxHealth;
            OnHealthPctChanged(currentHealthPct);
        }
    }
}
