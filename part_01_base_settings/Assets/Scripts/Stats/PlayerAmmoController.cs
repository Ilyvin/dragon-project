using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoController : MonoBehaviour
{
    public int maxAmmo = 50;
    public int currentAmmo = 5;
    public PlayerController player;
    //public int magazinLimit = 10;
    //public int currentMagazinAmmo = 10;
    //private int bulletCounter;
    
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }

    public int getCurrentAmmo()
    {
        return currentAmmo;
    }
    
    public void decreaseMagazin()
    {
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
            player.playerStats.setUserMessage("");
        }
        else
        {
            currentAmmo = result;
            player.playerStats.setUserMessage("");
        }
    }
}
