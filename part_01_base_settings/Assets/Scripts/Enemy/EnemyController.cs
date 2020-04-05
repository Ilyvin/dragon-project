using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody myRB;
    public float speed = 5f;
    public PlayerController player;
    public float attackingDistance = 3f;
    public float oneAttackTime = 3f;
    
    private IEnumerator attackingProcessCoroutine;
    private bool attackingProcessStarted = false;
    public float damage = 5f;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        myRB = GetComponent<Rigidbody>();
        attackingProcessCoroutine = attackingProcess(oneAttackTime);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackingDistance)
        {
            moveToPlayer();
        }
        else
        {
            attackPlayer();
        }
        
    }

    private void moveToPlayer()
    {
        myRB.velocity = transform.forward * speed;
        
        StopCoroutine(attackingProcessCoroutine);
        attackingProcessStarted = false;
    }

    private void attackPlayer()
    {
        myRB.velocity = Vector3.zero;

        if (!attackingProcessStarted)
        {
            StartCoroutine(attackingProcessCoroutine);
            attackingProcessStarted = true;
        }
    }

    void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }
    
    private IEnumerator attackingProcess(float waitTime)
    {
        while (true)
        {
            //print("Attack player " + Time.time + " damage: " + damage);
            player.GetComponent<PlayerHealthController>().changeHealth(-damage);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
