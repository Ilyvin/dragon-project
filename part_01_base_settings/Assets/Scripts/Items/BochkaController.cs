﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BochkaController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public int damage = 200;

    public float destroyTimeDelay = 2f;
    public GameObject aliveModel;
    public GameObject deadModel;
    private bool bochkaIsDestroyed = false;
    
    private AudioSource audioSource;
    public AudioClip getDamageSound;
    //public AudioClip destroySound;

    public GameObject normalExplosionEffect;
    private bool isExplosed = false;
    
    private PlayerController player;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    public void changeHealth(int healthDelta, PlayerController player)
    {
        int resultHealth = currentHealth + healthDelta;
        this.player = player;
        
        if (resultHealth <= 0)
        {
            currentHealth = 0;
            destroyBochka();
        }
        else if (resultHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            audioSource.clip = getDamageSound;
            audioSource.Play();
            currentHealth = resultHealth;
        }
    }

    private void destroyBochka()
    {
        if (!bochkaIsDestroyed)
        {
            bochkaIsDestroyed = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            //audioSource.clip = destroySound;
            //audioSource.Play();

            //isExplosed = true;

            instantiateExplosionEffect();

            //setDamageByRadius();

            aliveModel.SetActive(false);
            deadModel.SetActive(true);
            //damageArea.SetActive(true);

            Destroy(gameObject, destroyTimeDelay);
        }
        else
        {
            Debug.Log("Another Attempt to destroy Bochka");
        }
    }

    /*void OnCollisionEnter(Collision other)
    {
        if (isExplosed)
        {
            if (other.gameObject.tag == "Enemy")
            {
                //Debug.Log("Bochka has been destroyed and met Enemy: " + other);
                other.gameObject.GetComponent<EnemyHealthController>().changeHealth(-damage, player);
            }
            else if (other.gameObject.tag == "Bochka")
            {
                //Debug.Log("Bochka has been destroyed and met other Bochka: " + other);
                BochkaController bochkaController = other.gameObject.GetComponent<BochkaController>();
                bochkaController.changeHealth(-damage, player);
            }
            else if (other.gameObject.tag == "Player")
            {
                //Debug.Log("Bochka has been destroyed and met Player: " + other);
                other.gameObject.GetComponent<PlayerHealthController>().changeHealth(-damage);
            }
        }
    }*/

    private void instantiateExplosionEffect()
    {
        GameObject obj = Instantiate(normalExplosionEffect, transform.position, transform.rotation);
        obj.GetComponent<ExplosionController>().setDamage(damage);
        obj.GetComponent<ExplosionController>().setPlayer(player);
        
        //obj.explosion();
        Destroy(obj, 1.5f);
    }
}
