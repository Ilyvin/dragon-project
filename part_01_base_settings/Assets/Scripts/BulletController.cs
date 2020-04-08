using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public PlayerController player;
    private float lifetime = 10f;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("Bullet met trigger: " + other );
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Bullet met collision: " + other );
        
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy got damage = " + damage);
            other.gameObject.GetComponent<EnemyHealthController>().changeHealth(-damage, player);
            //щас сделано так, что пуля летит сквозь всех врагов, не останавливаясь.
            //этот эффект надо будет потом использовать для того, чтобы типо того это был мега-скилл бронебойного выстрела.
            //но для обычных выстрелов этот режим должен быть недоступен и пуля должна исчезнуть при первом же попадении во врага.
            
            Destroy(gameObject);
        }
    }
}