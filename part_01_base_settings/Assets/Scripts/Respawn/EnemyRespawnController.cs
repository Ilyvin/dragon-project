using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int respawnTime = 5;
    public int enemiesMaximumNumber = 10;
    

    void Start()
    {
        StartCoroutine(InstantiateEnemy(respawnTime, enemiesMaximumNumber));
    }
    
    
    // every 2 seconds perform the print()
    private IEnumerator InstantiateEnemy(int waitTime, int maximumOfEnemies)
    {
        int counter = 0;
        while (counter++ < maximumOfEnemies)
        {
            //print("InstantiateEnemy " + Time.time);
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
