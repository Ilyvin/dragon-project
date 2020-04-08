using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItemController : MonoBehaviour
{
    public int hillValue = 25;
    public float respawnDelay = 5f;
    
    private void hideItemObject()
    {
        gameObject.SetActive(false);
    }
    
    private void showItemObject()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AmmoItem");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player's ammo was extended: " + hillValue + " items");
            other.gameObject.GetComponent<PlayerAmmoController>().changeAmmo(hillValue);
            
            //Destroy(gameObject);
            hideItemObject();
            
            Invoke("showItemObject", respawnDelay);
        }
    }
}
