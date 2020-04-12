using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnHealthController : MonoBehaviour
{
    public float maxHealth = 200f;
    public int expaValue = 30;
    public float currentHealth;
    public event Action<float> OnHealthPctChanged = delegate { };
    private EnemyRespawnController enemyRespawnController;
    void Start()
    {
        currentHealth = maxHealth;
        enemyRespawnController = gameObject.GetComponent<EnemyRespawnController>();
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
            enemyRespawnController.enemyRespawnBaseDeath();
        }
        else if (resultHealth > maxHealth)
        {
            currentHealth = maxHealth;
            OnHealthPctChanged(maxHealth);
        }
        else
        {
            enemyRespawnController.playGetDamageSound();
            currentHealth = resultHealth;
            float currentHealthPct = (float) currentHealth / (float) maxHealth;
            OnHealthPctChanged(currentHealthPct);
        }
    }
}
