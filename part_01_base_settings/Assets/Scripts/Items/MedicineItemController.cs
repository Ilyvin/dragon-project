using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineItemController : MonoBehaviour
{
    public float hillValue = 25f;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("MedicineItem");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player was hilled by: " + hillValue + " hp");
            other.gameObject.GetComponent<PlayerHealthController>().changeHealth(hillValue);

            Destroy(gameObject);
        }
    }
}