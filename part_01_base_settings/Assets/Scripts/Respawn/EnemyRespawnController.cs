using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int respawnTime = 5;
    public int enemiesMaximumNumber = 10;
    private bool coroutineStarted = false;
    
    private IEnumerator InstantiateEnemy(int waitTime, int maximumOfEnemies)
    {
        int counter = 0;
        while (counter++ < maximumOfEnemies)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(InstantiateEnemy(respawnTime, enemiesMaximumNumber));
        }
    }
}
