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
    private EnemyController enemyController;
    void Start()
    {
        currentHealth = maxHealth;
        enemyController = gameObject.GetComponent<EnemyController>();
    }

    public void changeHealth(float healthDelta, PlayerController player)
    {
        float resultHealth = currentHealth + healthDelta;
        
        if (resultHealth <= 0)
        {
            currentHealth = 0;
            //Debug.Log("Enemy is dead");
            player.expaController.updateExpa(expaValue);
            Debug.Log("Player got Expa: " + expaValue);
            
            OnHealthPctChanged(0f);
            enemyController.enemyDeath();
        }
        else if (resultHealth > maxHealth)
        {
            currentHealth = maxHealth;
            OnHealthPctChanged(100f);
        }
        else
        {
            enemyController.playGetDamageSound();
            currentHealth = resultHealth;
            float currentHealthPct = (float) currentHealth / (float) maxHealth;
            OnHealthPctChanged(currentHealthPct);
        }
    }
}
