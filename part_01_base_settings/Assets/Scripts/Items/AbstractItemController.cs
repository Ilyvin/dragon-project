using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractItemController : MonoBehaviour
{
    protected AudioSource audioSource;
    public float respawnDelay = 5f;
    public GameObject childModel;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected void hideItemObject()
    {
        childModel.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
    }
    
    protected void showItemObject()
    {
        childModel.SetActive(true);
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
