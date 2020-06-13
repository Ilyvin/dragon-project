using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoController : MonoBehaviour
{
    public int maxAmmo = 50;
    public int currentAmmo = 5;
    private PlayerController playerController;
    
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    public int getCurrentAmmo()
    {
        return currentAmmo;
    }
    
    public int getAmmoForMagazin(int ammo)
    {
        int result = -1;
        
        if (currentAmmo <= 0)
        {
            result = 0;
        }
        
        if (currentAmmo - ammo <= 0)
        {
            result = currentAmmo;
            currentAmmo = 0;
        }
        
        if (currentAmmo - ammo > 0)
        {
            currentAmmo -= ammo;
            result = ammo;
        }

        playerController.playerStats.setAmmoValue(currentAmmo);
        return result;
    }

    public void changeAmmo(int ammoDelta)
    {
        int result = currentAmmo + ammoDelta;
        
        if (result <= 0)
        {
            currentAmmo = 0;
        }
        else if (result > maxAmmo)
        {
            currentAmmo = maxAmmo;
            playerController.setUserMessage("");
        }
        else
        {
            currentAmmo = result;
            playerController.setUserMessage("");
        }
        
        playerController.playerStats.setAmmoValue(currentAmmo);
    }
}
