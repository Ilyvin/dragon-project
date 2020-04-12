using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text hpValue;
    public Text ammoValue;
    public Text magazinAmmoValue;
    public Text expaValue;
    public Text userMessage;
    private PlayerHealthController healthController;
    private PlayerAmmoController ammoController;
    private GunController gunController;
    private PlayerExperienceController expaController;

    void Start()
    {
        healthController = gameObject.GetComponent<PlayerHealthController>();
        ammoController = gameObject.GetComponent<PlayerAmmoController>();
        gunController = gameObject.GetComponent<PlayerController>().gunController;
        expaController = gameObject.GetComponent<PlayerExperienceController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hpValue.GetComponent<Text>().text = healthController.currentHealth.ToString();
        ammoValue.GetComponent<Text>().text = ammoController.currentAmmo.ToString();
        magazinAmmoValue.GetComponent<Text>().text = gunController.currentMagazinAmmo.ToString();
        expaValue.GetComponent<Text>().text = expaController.currentExpa.ToString();
    }

    public void setUserMessage(String msg)
    {
        userMessage.GetComponent<Text>().text = msg;
    }
}