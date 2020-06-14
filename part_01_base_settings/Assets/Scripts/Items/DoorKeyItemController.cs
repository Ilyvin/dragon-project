using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyItemController : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject childModel;
    public DoorKeyColor doorKeyColor;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void hideItemObject()
    {
        childModel.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("DoorKeyItem");
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            //Debug.Log("Player received DoorKeyItem: " + doorKeyColor);
            other.gameObject.GetComponent<DoorKeysContainer>().addDoorKey(doorKeyColor);

            //Destroy(gameObject);
            
            hideItemObject();
        }
    }
}
