using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronikItemController : MonoBehaviour
{
    private AudioSource audioSource;
    public float respawnDelay = 5f;
    public GameObject childModel;
    
    void Start()
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
            other.gameObject.GetComponent<PlayerHealthController>().activateBronik(true);

            //Destroy(gameObject);
            
            hideItemObject();
            //механизм респауна объекта после того, как он был подобран игроком
            Invoke("showItemObject", respawnDelay);
        }
    }
}
