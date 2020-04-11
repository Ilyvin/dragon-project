using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject musicVolumeObject;
    public GameObject masterVolumeObject;
    public float masterVolume;
    public float musicVolume;

    void Start()
    {
        if (masterVolume != null && musicVolume != null)
        {
            audioMixer.SetFloat("masterVolume", masterVolume);
            audioMixer.SetFloat("musicVolume", musicVolume);

            masterVolumeObject.GetComponent<Slider>().value = masterVolume;
            musicVolumeObject.GetComponent<Slider>().value = musicVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}