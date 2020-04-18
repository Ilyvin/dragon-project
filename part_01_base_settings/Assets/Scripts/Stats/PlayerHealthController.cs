using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public PlayerController player;

    void Start()
    {
        currentHealth = maxHealth;
        player = gameObject.GetComponent<PlayerController>();
        
    }

    public void changeHealth(float healthDelta)
    {
        float resultHealth = currentHealth + healthDelta;

        if (resultHealth <= 0)
        {
            currentHealth = 0;
            player.playerStats.setUserMessage("Чувак, ты оглушён");
            //Debug.Log("****************Чувак, ты оглушён");
            player.playerRespawnNeeded = true;
        }
        else if (resultHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = resultHealth;
        }
        
    }
    
}