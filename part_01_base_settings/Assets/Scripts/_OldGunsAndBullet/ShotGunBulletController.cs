using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBulletController : MonoBehaviour
{
    public float speed = 10f;
    public PlayerController player;
    private float lifetime = 10f;
    private float damage;
    private bool isArmorPiercing; //бронебойный
    

    public int numberOfBullets = 10;
    public float courseDeviationRandomRange = 0.1f;
    public float speedDeviationRandomRange = 0.1f;
    public Vector3 direction;
    public Quaternion rotation;
    

    public GameObject bulletPrefab;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void initBullets()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            Instantiate(bulletPrefab, direction, rotation);
        }
    }

    public void setPlayerController(PlayerController playerController)
    {
        player = playerController;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }
    
    public void setIsArmorPiercing(bool isArmorPiercing)
    {
        this.isArmorPiercing = isArmorPiercing;
    }
    


    
}
