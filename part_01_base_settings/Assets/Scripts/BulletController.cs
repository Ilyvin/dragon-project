using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public PlayerController player;
    private float lifetime = 10f;
    private float damage;
    
    public float bulletHolePositionOffset = 1.5f;
    public GameObject bulletWallHolePrefab;
    public GameObject bulletBloodHolePrefab;
    public GameObject bulletHoleEnemyBasePrefab;
    public GameObject enemyBloodPuddlePrefab;
    private GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Destroy(gameObject, lifetime);
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OLOLO Bullet met trigger: " + other );
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Bullet met collision: " + other );
        
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthController>().changeHealth(-damage, player);
            //щас сделано так, что пуля летит сквозь всех врагов, не останавливаясь.
            //этот эффект надо будет потом использовать для того, чтобы типо того это был мега-скилл бронебойного выстрела.
            //но для обычных выстрелов этот режим должен быть недоступен и пуля должна исчезнуть при первом же попадении во врага.

            if (gameController.violenceEnabled)
            {
                GameObject bulletHole = Instantiate(bulletBloodHolePrefab, 
                    transform.position - transform.forward * bulletHolePositionOffset, 
                    transform.rotation);
                bulletHole.transform.parent = other.gameObject.GetComponent<EnemyController>().aliveModel.transform;

                GameObject bloodPuddle =
                    Instantiate(enemyBloodPuddlePrefab, transform.position, Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)));
            }
            
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "EnemyRespawnBase")
        {
            other.gameObject.GetComponent<EnemyRespawnHealthController>().changeHealth(-damage, player);
            GameObject bulletHole = Instantiate(bulletHoleEnemyBasePrefab, transform.position - transform.forward * bulletHolePositionOffset, transform.rotation);
            bulletHole.transform.parent = other.transform;
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Bullet met Wall");
            GameObject bulletHole = Instantiate(bulletWallHolePrefab, transform.position - transform.forward * bulletHolePositionOffset, transform.rotation);
            bulletHole.transform.parent = other.transform;
            Destroy(gameObject);
        }
    }
}