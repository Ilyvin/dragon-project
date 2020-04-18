using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    private AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        doorAnimator = gameObject.GetComponentInChildren<Animator>();
        doorAnimator.SetBool("closed", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Door Open");
        if (other.gameObject.tag == "Player")
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("closed", false);
                audioSource.clip = openSound;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Door Close");
        if (other.gameObject.tag == "Player")
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("closed", true);
                audioSource.clip = closeSound;
                audioSource.Play();
            }
        }
    }
}
