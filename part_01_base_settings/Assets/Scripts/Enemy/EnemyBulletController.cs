using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBulletType
{
    BAZUKA_ROCKET,
    BULLET
}
public class EnemyBulletController : MonoBehaviour
{
    public float speed = 10f;
    private float lifetime = 10f;
    private int damage;

    public float bulletHolePositionOffset = 1.5f;
    public GameObject bulletWallHolePrefab;
    public GameObject bulletBloodHolePrefab;
    public GameObject enemyBloodPuddlePrefab;
    private GameController gameController;
    private EnemyBulletType enemyBulletType = EnemyBulletType.BULLET;

    public GameObject bazukaRocketExplosionEffect;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //Debug.Log("[Bullet Controller] game Controller: " + gameController);
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnemyBullet met collision: " + other);

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("EnemyBullet met Player");
            
            other.gameObject.GetComponent<PlayerHealthController>().changeHealth(-damage);

            if (gameController == null)
            {
                Debug.LogError("!!! gameController == null");
            }
            
            if (gameController != null && gameController.violenceEnabled)
            {
                GameObject bulletHole = Instantiate(bulletBloodHolePrefab,
                    transform.position - transform.forward * bulletHolePositionOffset,
                    transform.rotation);
                bulletHole.transform.parent = other.gameObject.GetComponent<PlayerController>().transform;

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

            if (enemyBulletType == EnemyBulletType.BAZUKA_ROCKET)
            {
                instantiateExplosionEffect();
            }
            
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Wall")
        {
            //Debug.Log("Bullet met Wall");
            GameObject bulletHole = Instantiate(bulletWallHolePrefab,
                transform.position - transform.forward * bulletHolePositionOffset, transform.rotation);
            bulletHole.transform.parent = other.transform;

            if (enemyBulletType == EnemyBulletType.BAZUKA_ROCKET)
            {
                instantiateExplosionEffect();
            }

            Destroy(gameObject);
        }
    }

    private void instantiateExplosionEffect()
    {
        GameObject obj = Instantiate(bazukaRocketExplosionEffect, transform.position, transform.rotation);
        Destroy(obj, 1.5f);
    }
}
