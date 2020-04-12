using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int respawnTime = 5;
    public int enemiesMaximumNumber = 10;
    private bool coroutineStarted = false;

    //public PlayerController player;

    public GameObject aliveModel;
    public GameObject deadModel;

    private AudioSource audioSource;
    [SerializeField] public AudioClip[] newEnemyRespawnSoundsArray;
    [SerializeField] public AudioClip[] deathSoundsArray;
    [SerializeField] public AudioClip[] getDamageSoundsArray;

    public float newEnemyRespawnSoundVolume = 0.3f;
    public float deathSoundVolume = 0.5f;
    public float getDamageSoundVolume = 0.5f;

    public float destroyDelay = 10f;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        aliveModel.SetActive(true);
        deadModel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator InstantiateEnemy(int waitTime, int maximumOfEnemies)
    {
        int counter = 0;
        while (counter++ < maximumOfEnemies)
        {
            audioSource.volume = newEnemyRespawnSoundVolume;
            audioSource.clip = newEnemyRespawnSoundsArray[Random.Range(0, deathSoundsArray.Length)];
            audioSource.Play();
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(InstantiateEnemy(respawnTime, enemiesMaximumNumber));
        }
    }

    public void enemyRespawnBaseDeath()
    {
        aliveModel.SetActive(false);
        deadModel.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = false;

        AudioClip clip = deathSoundsArray[Random.Range(0, deathSoundsArray.Length)];
        audioSource.clip = clip;
        audioSource.volume = deathSoundVolume;
        audioSource.Play();

        Invoke("destroyEnemyRespawnBase", destroyDelay);
    }

    private void destroyEnemyRespawnBase()
    {
        Destroy(gameObject);
    }

    public void playGetDamageSound()
    {
        AudioClip clip = getDamageSoundsArray[Random.Range(0, getDamageSoundsArray.Length)];
        audioSource.clip = clip;
        audioSource.volume = getDamageSoundVolume;
        audioSource.Play();
    }
}