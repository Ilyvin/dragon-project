﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    //public AudioMixerGroup audioMixerGroup;

    /*public void setVolumeRU(float volume)
    {
        Debug.Log("setVolume RU " + volume);
        audioMixerGroup.audioMixer.SetFloat("volume", volume);
    }

    public void toggleVolumeRU(bool mute)
    {
        Debug.Log("toggleVolume RU " + mute);
        audioMixerGroup.audioMixer.SetFloat("volume", mute ? 0 : 80);
    }*/


    public void setVolume(float volume)
    {
        Debug.Log("setVolume " + volume);
        audioMixer.SetFloat("volume", volume);
    }

    public void setQuality(int qualityIndex)
    {
        Debug.Log("setQuality " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Debug.Log("setFullScreen " + isFullScreen);
        Screen.fullScreen = isFullScreen;
    }
}