using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public PlayerController player;
    private float lifetime = 10f;
    private int damage;
    private bool isArmorPiercing = false; //бронебойный
    private int enemiesPiercingCounter; //счётчик убитых насквозь врагов
    private int enemiesPiercingLimit; //количество врагов, которых можно убить подряд насквозь

    public float bulletHolePositionOffset = 1.5f;
    public GameObject bulletWallHolePrefab;
    public GameObject bulletBloodHolePrefab;
    public GameObject bulletHoleEnemyBasePrefab;
    public GameObject enemyBloodPuddlePrefab;
    private GameController gameController;
    private BulletType bulletType = BulletType.BULLET;

    public GameObject bazukaRocketExplosionEffect;
    public GameObject damageArea;
    
    private bool isExplosed = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //Debug.Log("[Bullet Controller] game Controller: " + gameController);
        Destroy(gameObject, lifetime);
        enemiesPiercingCounter = 0;
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

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public void setBulletType(BulletType bulletType)
    {
        this.bulletType = bulletType;
    }

    public void setIsArmorPiercing(bool isArmorPiercing)
    {
        this.isArmorPiercing = isArmorPiercing;
    }

    public void setEnemiesPiercingLimit(int enemiesPiercingLimit)
    {
        this.enemiesPiercingLimit = enemiesPiercingLimit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isExplosed)
        {
            Debug.Log("BULLET is EXPLODED!");
            if (bulletType == BulletType.BAZUKA_ROCKET)
            {
                Debug.Log("BULLET is EXPLODED! - IT's BAZUKA");
                Debug.Log("BULLET is EXPLODED! - met object: " + other.gameObject.tag);

                if (other.gameObject.tag == "Enemy")
                {
                    Debug.Log("BULLET BAZUKA has been destroyed and met Enemy: " + other);
                    other.gameObject.GetComponent<EnemyHealthController>().changeHealth(-damage, player);
                }
                else if (other.gameObject.tag == "Bochka")
                {
                    Debug.Log("BULLET BAZUKA has been destroyed and met other Bochka: " + other);
                    BochkaController bochkaController = other.gameObject.GetComponent<BochkaController>();
                    bochkaController.changeHealth(-damage, player);
                }
                else if (other.gameObject.tag == "Player")
                {
                    Debug.Log("BULLET BAZUKA  has been destroyed and met Player: " + other);
                    other.gameObject.GetComponent<PlayerHealthController>().changeHealth(-damage);
                }
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Bullet met collision: " + other);

        if (!isExplosed)
        {
            Debug.Log("BULLET is NOT exposed!");
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyHealthController>().changeHealth(-damage, player);
                //щас сделано так, что пуля летит сквозь всех врагов, не останавливаясь.
                //этот эффект надо будет потом использовать для того, чтобы типо того это был мега-скилл бронебойного выстрела.
                //но для обычных выстрелов этот режим должен быть недоступен и пуля должна исчезнуть при первом же попадении во врага.

                if (gameController == null)
                {
                    Debug.LogError("!!! gameController == null");
                }

                if (gameController != null && gameController.violenceEnabled)
                {
                    GameObject bulletHole = Instantiate(bulletBloodHolePrefab,
                        transform.position - transform.forward * bulletHolePositionOffset,
                        transform.rotation);
                    bulletHole.transform.parent = other.gameObject.GetComponent<EnemyController>().aliveModel.transform;

                    GameObject bloodPuddle =
                        Instantiate(enemyBloodPuddlePrefab, transform.position,
                            Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)));

                    //поставим размер лужи в соответствии с уроном
                    Projector projector = bloodPuddle.GetComponent<Projector>();
                    if (0 < damage && damage <= 20)
                    {
                        projector.orthographicSize = 1;
                    }

                    if (20 < damage && damage <= 40)
                    {
                        projector.orthographicSize = 3;
                    }

                    if (40 < damage && damage <= 60)
                    {
                        projector.orthographicSize = 5;
                    }

                    if (60 < damage && damage <= 80)
                    {
                        projector.orthographicSize = 7;
                    }

                    if (80 < damage && damage <= 100)
                    {
                        projector.orthographicSize = 10;
                    }
                }

                enemiesPiercingCounter++;
                //Debug.Log("enemiesPiercingCounter = " + enemiesPiercingCounter);

                if (!isArmorPiercing)
                {
                    if (bulletType == BulletType.BAZUKA_ROCKET)
                    {
                        instantiateExplosionEffect();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                } //подсчёт, когда остановить бронебойную пулю (enemiesPiercingLimit - число врагов, убитых подряд)
                else if (enemiesPiercingCounter >= enemiesPiercingLimit)
                {
                    Destroy(gameObject);
                }
            }
            else if (other.gameObject.tag == "EnemyRespawnBase")
            {
                other.gameObject.GetComponent<EnemyRespawnHealthController>().changeHealth(-damage, player);
                GameObject bulletHole = Instantiate(bulletHoleEnemyBasePrefab,
                    transform.position - transform.forward * bulletHolePositionOffset, transform.rotation);
                bulletHole.transform.parent = other.transform;

                if (bulletType == BulletType.BAZUKA_ROCKET)
                {
                    instantiateExplosionEffect();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (other.gameObject.tag == "Wall")
            {
                //Debug.Log("Bullet met Wall");
                GameObject bulletHole = Instantiate(bulletWallHolePrefab,
                    transform.position - transform.forward * bulletHolePositionOffset, transform.rotation);
                bulletHole.transform.parent = other.transform;

                if (bulletType == BulletType.BAZUKA_ROCKET)
                {
                    instantiateExplosionEffect();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (other.gameObject.tag == "Bochka")
            {
                //Debug.Log("Bullet met Bochka");
                BochkaController bochkaController = other.gameObject.GetComponent<BochkaController>();
                bochkaController.changeHealth(-damage, player);

                if (bulletType == BulletType.BAZUKA_ROCKET)
                {
                    instantiateExplosionEffect();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            /*Debug.Log("BULLET is EXPLODED!");
            if (bulletType == BulletType.BAZUKA_ROCKET)
            {
                Debug.Log("BULLET is EXPLODED! - IT's BAZUKA");
                Debug.Log("BULLET is EXPLODED! - met object: " + other.gameObject.tag);

                if (other.gameObject.tag == "Enemy")
                {
                    Debug.Log("BULLET BAZUKA has been destroyed and met Enemy: " + other);
                    other.gameObject.GetComponent<EnemyHealthController>().changeHealth(-damage, player);
                }
                else if (other.gameObject.tag == "Bochka")
                {
                    Debug.Log("BULLET BAZUKA has been destroyed and met other Bochka: " + other);
                    BochkaController bochkaController = other.gameObject.GetComponent<BochkaController>();
                    bochkaController.changeHealth(-damage, player);
                }
                else if (other.gameObject.tag == "Player")
                {
                    Debug.Log("BULLET BAZUKA  has been destroyed and met Player: " + other);
                    other.gameObject.GetComponent<PlayerHealthController>().changeHealth(-damage);
                }
            }*/
        }
    }
  
    private void instantiateExplosionEffect()
    {
        GameObject obj = Instantiate(bazukaRocketExplosionEffect, transform.position, transform.rotation);
        Destroy(obj, 1.5f);
        isExplosed = true;
        damageArea.SetActive(true);
        
        Destroy(gameObject, 1.5f);
    }
}