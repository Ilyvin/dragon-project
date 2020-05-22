using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public GameObject targetTeleport;
    public bool teleportIsActive = true;
    void Start()
    {
        teleportIsActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && teleportIsActive)
        {
            targetTeleport.GetComponent<TeleportController>().teleportIsActive = false;
            other.GetComponent<PlayerController>().moveToPosition(targetTeleport.transform.position);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            teleportIsActive = true;
        }
    }
}
