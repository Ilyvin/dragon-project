using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private PlayerController playerController;

    void Start()
    {
        currentHealth = maxHealth;
        playerController = gameObject.GetComponent<PlayerController>();
        
    }

    public void changeHealth(float healthDelta)
    {
        float resultHealth = currentHealth + healthDelta;

        if (resultHealth <= 0)
        {
            currentHealth = 0;
            playerController.playerStats.setUserMessage("Чувак, ты оглушён");
            //Debug.Log("****************Чувак, ты оглушён");
            playerController.playerRespawnNeeded = true;
        }
        else if (resultHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = resultHealth;
        }
        
        playerController.playerStats.setHealthValue(currentHealth);
    }
    
}