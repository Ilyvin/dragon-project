using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public int damage;
    public float destroyTimeDelay = 2f;
    
    private AudioSource audioSource;
    public AudioClip destroySound;
    
    public GameObject normalExplosionEffect;
    public PlayerController player;
    public float hideCollidersDelay = 0.1f;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        
        instantiateExplosionEffect();
        audioSource.clip = destroySound;
        audioSource.Play();
        Invoke("hideColliders", hideCollidersDelay);
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }


    private void hideColliders()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
    
    public void setPlayer(PlayerController player)
    {
        this.player = player;
    }
    
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Explosion has been activated and met Enemy: " + other);
            other.gameObject.GetComponent<EnemyHealthController>().changeHealth(-damage, player);
        }
        else if (other.gameObject.tag == "Bochka")
        {
            Debug.Log("Bochka has been destroyed and met other Bochka: " + other);
            BochkaController bochkaController = other.gameObject.GetComponent<BochkaController>();
            bochkaController.changeHealth(-damage, player);
        }
        else if (other.gameObject.tag == "Player")
        {
            Debug.Log("Explosion has been activated and met Player: " + other);
            other.gameObject.GetComponent<PlayerHealthController>().changeHealth(-damage);
        }
    }
    
    private void instantiateExplosionEffect()
    {
        GameObject obj = Instantiate(normalExplosionEffect, transform.position, transform.rotation);
        Destroy(obj, 1.5f);
    }
}
