﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineItemController : MonoBehaviour
{
    public int hillValue = 25;
    public float respawnDelay = 5f;
    private AudioSource audioSource;
    public GameObject childModel;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    private void hideItemObject()
    {
        childModel.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    
    private void showItemObject()
    {
        childModel.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("MedicineItem");
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            //Debug.Log("Player was hilled by: " + hillValue + " hp");
            other.gameObject.GetComponent<PlayerHealthController>().changeHealth(hillValue);

            //Destroy(gameObject);
            
            hideItemObject();
            //механизм респауна объекта после того, как он был подобран игроком
            Invoke("showItemObject", respawnDelay);
        }
    }
}