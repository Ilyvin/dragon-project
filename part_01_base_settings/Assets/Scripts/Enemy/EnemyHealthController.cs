using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int maxHealth = 100;
    public int expaValue = 10;
    public int currentHealth;
    public event Action<float> OnHealthPctChanged = delegate { };
    private EnemyController enemyController;
    void Start()
    {
        currentHealth = maxHealth;
        enemyController = gameObject.GetComponent<EnemyController>();
    }

    public void changeHealth(int healthDelta, PlayerController player)
    {
        int resultHealth = currentHealth + healthDelta;
        
        if (resultHealth <= 0)
        {
            currentHealth = 0;
            //Debug.Log("Enemy is dead");
            if (player != null)
            {
                //Debug.Log("Player is not null");
                if (player.expaController != null)
                {
                    //Debug.Log("Player.expaController is not null");
                    //Debug.Log("Player got Expa: " + expaValue);
                    player.expaController.updateExpa(expaValue);
                }
                else
                {
                    //Debug.Log("Player.expaController is null");
                }
            }
            
            OnHealthPctChanged(0f);
            enemyController.enemyDeath();
        }
        else if (resultHealth > maxHealth)
        {
            currentHealth = maxHealth;
            OnHealthPctChanged(maxHealth);
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
