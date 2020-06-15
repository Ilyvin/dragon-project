using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealthController : MonoBehaviour
{
    public int maxHealth = 100;
    public int expaValue = 10;
    public int currentHealth;
    public event Action<float> OnHealthPctChanged = delegate { };
    
    private EnemyController enemyController;
    private GameController gameController;
    public GameObject enemyBloodPuddlePrefab;
    private int maxBloodPuddleOrthographicSize = 8;
    
    void Start()
    {
        currentHealth = maxHealth;
        enemyController = gameObject.GetComponent<EnemyController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void changeHealth(int healthDelta, PlayerController player)
    {
        int resultHealth = currentHealth + healthDelta;

        //если это ранение, а не аптечка
        if (healthDelta < 0)
        {
            showBloodPuddle(healthDelta);
        }

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
            float currentHealthPct = (float) currentHealth / maxHealth;
            OnHealthPctChanged(currentHealthPct);
        }
    }

    private void showBloodPuddle(int damage)
    {
        if (gameController == null)
                        {
                            Debug.LogError("!!! gameController == null");
                        }

        if (gameController != null && gameController.violenceEnabled)
        {
            GameObject bloodPuddle =
                Instantiate(enemyBloodPuddlePrefab, 
                    new Vector3(transform.position.x, 1, transform.position.z),
                    Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)));
            
            Projector projector = bloodPuddle.GetComponent<Projector>();

            //поставим размер лужи в соответствии с процентом урона от здоровья
            int damagePercent = Math.Abs(damage) / maxHealth * maxBloodPuddleOrthographicSize;
            int orthographicSize;

            if (damagePercent > maxBloodPuddleOrthographicSize)
            {
                orthographicSize = maxBloodPuddleOrthographicSize;
            }
            else if (damagePercent < maxBloodPuddleOrthographicSize)
            {
                orthographicSize = 1;
            }
            else
            {
                orthographicSize = damagePercent;
            }
            
            projector.orthographicSize = orthographicSize;
        }
    }
}
