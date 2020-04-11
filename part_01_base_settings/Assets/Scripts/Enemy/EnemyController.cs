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

    public GameObject aliveModel;
    public GameObject deadModel;

    private AudioSource audioSource;
    [SerializeField] public AudioClip[] stepsSoundsArray;
    [SerializeField] public AudioClip[] deathSoundsArray;
    [SerializeField] public AudioClip[] attackSoundsArray;
    [SerializeField] public AudioClip[] getDamageSoundsArray;

    public float stepsSoundVolume = 0.3f;
    public float deathSoundVolume = 0.5f;
    public float attackSoundVolume = 0.5f;
    public float getDamageSoundVolume = 0.5f;
    
    public float destroyDelay = 10f;
    private bool isDead = false;
    
    public float timeBetweenSteps = 0.3f;
    public float timer = 0f;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        myRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        attackingProcessCoroutine = attackingProcess(oneAttackTime);
        
        aliveModel.SetActive(true);
        deadModel.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackingDistance)
            {
                moveToPlayer();
                playStepSound();
            }
            else
            {
                attackPlayer();
            }
        }
        else
        {
            myRB.velocity = Vector3.zero;
        }
    }

    public void enemyDeath()
    {
        aliveModel.SetActive(false);
        deadModel.SetActive(true);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        isDead = true;
        
        AudioClip clip = deathSoundsArray[Random.Range(0, deathSoundsArray.Length)];
        audioSource.clip = clip;
        audioSource.volume = deathSoundVolume;
        audioSource.Play();
        
        Invoke("destroyEnemy", destroyDelay);
    }
    
    
    private void playStepSound()
    {
        if (new Vector3(myRB.velocity.x, 0, myRB.velocity.z).magnitude > 0.2f)
        {
            timer += Time.deltaTime;

            if(timer > timeBetweenSteps)
            {
                timer = 0;
                AudioClip clip = stepsSoundsArray[Random.Range(0, stepsSoundsArray.Length)];
                audioSource.clip = clip;
                audioSource.volume = stepsSoundVolume;
                audioSource.Play();
            }
        }
    }

    public void playGetDamageSound()
    {
        AudioClip clip = getDamageSoundsArray[Random.Range(0, getDamageSoundsArray.Length)];
        audioSource.clip = clip;
        audioSource.volume = getDamageSoundVolume;
        audioSource.Play();
    }
    
    private void destroyEnemy()
    {
        Destroy(gameObject);
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

    private IEnumerator attackingProcess(float waitTime)
    {
        while (true)
        {
            //print("Attack player " + Time.time + " damage: " + damage);
            AudioClip clip = attackSoundsArray[Random.Range(0, attackSoundsArray.Length)];
            audioSource.clip = clip;
            audioSource.volume = attackSoundVolume;
            audioSource.Play();
            player.GetComponent<PlayerHealthController>().changeHealth(-damage);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
