using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public float currentHealth;
    public int currentExpa;
    public int currentAmmo;

    public float[] position;
    
    public PlayerData(PlayerController playerController)
    {
        currentHealth = playerController.healthController.currentHealth;
        currentExpa = playerController.expaController.currentExpa;
        currentAmmo = playerController.ammoController.currentAmmo;

        position = new float[3];
        position[0] = playerController.transform.position.x;
        position[1] = playerController.transform.position.y;
        position[2] = playerController.transform.position.z;

    }
}