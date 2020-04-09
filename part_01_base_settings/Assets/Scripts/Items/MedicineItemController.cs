using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineItemController : MonoBehaviour
{
    public float hillValue = 25f;
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
        Debug.Log("MedicineItem");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player was hilled by: " + hillValue + " hp");
            other.gameObject.GetComponent<PlayerHealthController>().changeHealth(hillValue);

            //Destroy(gameObject);
            
            hideItemObject();
            //механизм респауна объекта после того, как он был подобран игроком
            Invoke("showItemObject", respawnDelay);
        }
    }
}