using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private PlayerController playerController;

    private bool bronikIsActive = false;
    public int maxBronik = 100;
    public int currentBronik;
    //public Sprite bronikIcon;

    void Start()
    {
        currentHealth = maxHealth;
        currentBronik = 0;
        playerController = gameObject.GetComponent<PlayerController>();
        bronikIsActive = false;
    }

    public void changeHealth(int healthDelta)
    {
        if (healthDelta < 0)
        {
            if (bronikIsActive && currentBronik > 0)
            {
                int resultBronikHealth = currentBronik + healthDelta;

                if (resultBronikHealth <= 0)
                {
                    activateBronik(false);
                    decreaseHealth(resultBronikHealth);
                }
                else
                {
                    currentBronik = resultBronikHealth;
                }
            }
            else
            {
                decreaseHealth(healthDelta);
            }
        }
        else
        {
            increaseHealth(healthDelta);
        }
        
        playerController.playerStats.setHealthValue(currentHealth);
        playerController.playerStats.setBronikValue(currentBronik);
    }

    private void increaseHealth(int healthDelta)
    {
        int resultHealth = currentHealth + healthDelta;

        if (resultHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = resultHealth;
        }
    }

    private void decreaseHealth(int delta)
    {
        int resultHealth = currentHealth + delta;

        if (resultHealth <= 0)
        {
            currentHealth = 0;
            playerController.playerStats.setUserMessage("Чувак, ты оглушён");
            //Debug.Log("****************Чувак, ты оглушён");
            playerController.playerRespawnNeeded = true;
        }
        else
        {
            currentHealth = resultHealth;
        }
    }

    public void activateBronik(bool flag)
    {
        Debug.Log("activateBronik...");
        
        if (flag)
        {
            currentBronik = maxBronik;
        }
        else
        {
            currentBronik = 0;
        }
        Debug.Log("currentBronik = " + currentBronik);
        Debug.Log("maxBronik = " + maxBronik);
        
        bronikIsActive = flag;
        playerController.playerStats.setBronikValue(currentBronik);
        playerController.playerStats.showBronik(flag);
    }
}