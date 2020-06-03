using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorKeyColor
{
    NO_COLOR,
    RED,
    GREEN,
    BLUE,
    YELLOW,
    BLACK
}
public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    private AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    public DoorKeyColor doorKeyColor = DoorKeyColor.NO_COLOR;
    private PlayerController player;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        doorAnimator = gameObject.GetComponentInChildren<Animator>();
        doorAnimator.SetBool("closed", true);
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
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
            if (DoorKeyColor.NO_COLOR.Equals(doorKeyColor))
            {
                if (doorAnimator != null)
                {
                    doorAnimator.SetBool("closed", false);
                    audioSource.clip = openSound;
                    audioSource.Play();
                }
            }else
            {
                if (player.doorKeysContainer.containsKey(doorKeyColor))
                {
                    doorAnimator.SetBool("closed", false);
                    audioSource.clip = openSound;
                    audioSource.Play();
                }
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
