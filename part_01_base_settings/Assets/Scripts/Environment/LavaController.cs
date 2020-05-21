using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    private AudioSource audioSource;
    public int damage = 5;
    public float nextTimeToDamage = 0f;
    public float damageInterval = 0.5f;
    private IEnumerator attackingProcessCoroutine;
    private PlayerHealthController playerHealth;
    
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        attackingProcessCoroutine = attackingProcess(damageInterval);
    }
    
    /*private void Update()
    {
        nextTimeToDamage = Time.time + damageInterval;
    }*/
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("AmmoItem");
        if (other.gameObject.tag == "Player")
        {

            playerHealth = other.gameObject.GetComponent<PlayerHealthController>();
            StartCoroutine(attackingProcessCoroutine);
            /*while (true)
            {
                if (Time.time >= nextTimeToDamage)
                {
                    nextTimeToDamage = Time.time + damageInterval;
                    Debug.Log("Player's ammo was decreased by lava: " + damage);
                    other.gameObject.GetComponent<PlayerHealthController>().changeHealth(-damage);
                }
            }*/
        }
    }
    
    private IEnumerator attackingProcess(float waitTime)
    {
        while (true)
        {
            //if (Time.time >= nextTimeToDamage)
            {
                //nextTimeToDamage = Time.time + damageInterval;
                Debug.Log("Player's ammo was decreased by lava: " + damage);
                playerHealth.changeHealth(-damage);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine(attackingProcessCoroutine);
        }
    }
}
