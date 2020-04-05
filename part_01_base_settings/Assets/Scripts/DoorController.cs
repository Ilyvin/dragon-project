using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    void Start()
    {
        doorAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door Open");
        if (other.gameObject.tag == "Player")
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("open", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Door Close");
        if (other.gameObject.tag == "Player")
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("open", true);
            }
        }
    }
}
