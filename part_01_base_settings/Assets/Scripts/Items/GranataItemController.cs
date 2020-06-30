using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranataItemController : AbstractItemController
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("MedicineItem");
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            //Debug.Log("Player was hilled by: " + hillValue + " hp");
            //other.gameObject.GetComponent<PlayerHealthController>().activateBronik(true);

            //Destroy(gameObject);

            hideItemObject();
            //механизм респауна объекта после того, как он был подобран игроком
            Invoke("showItemObject", respawnDelay);
        }
    }
}