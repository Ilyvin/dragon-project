using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] public AudioClip[] stepsAudioArray;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void playOneStep()
    {
        AudioClip clip = stepsAudioArray[Random.Range(0, stepsAudioArray.Length)];
        audioSource.clip = clip;
        audioSource.Play();
    }
}