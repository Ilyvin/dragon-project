using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperienceController : MonoBehaviour
{
    public int currentExpa;
    public PlayerController playerController;

    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void updateExpa(int delta)
    {
        currentExpa += delta;

        playerController.playerStats.setExpaValue(currentExpa);
    }
}