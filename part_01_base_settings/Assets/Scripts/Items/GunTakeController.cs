using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTakeController : MonoBehaviour
{
    public int ammoValue = 10;
    public float respawnDelay = 5f;
    private AudioSource audioSource;
    public GameObject gunModel;
    public GameObject gunPrefab;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void hideItemObject()
    {
        gunModel.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    
    private void showItemObject()
    {
        gunModel.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("AmmoItem");
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            //Debug.Log("Player's ammo was extended: " + ammoValue + " items");
            //Debug.Log("Player got new weapon");
            other.gameObject.GetComponent<PlayerAmmoController>().changeAmmo(ammoValue);
            other.gameObject.GetComponent<PlayerController>().newGunController.fillMagazinIfEmpty();
            other.gameObject.GetComponent<PlayerController>().changeWeaponController.addNewWeapon(gunPrefab);
            
            //Destroy(gameObject);
            hideItemObject();
            //механизм респауна объекта после того, как он был подобран игроком
            Invoke("showItemObject", respawnDelay);
        }
    }
}
