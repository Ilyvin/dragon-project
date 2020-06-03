using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeysContainer : MonoBehaviour
{
    public HashSet<DoorKeyColor> collectedKeys = new HashSet<DoorKeyColor>();
    private PlayerController player;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void addDoorKey(DoorKeyColor key)
    {
        collectedKeys.Add(key);
        if (DoorKeyColor.RED.Equals(key))
        {
            player.playerStats.showRedKey(true);
        }
        if (DoorKeyColor.BLUE.Equals(key))
        {
            player.playerStats.showBlueKey(true);
        }
        if (DoorKeyColor.YELLOW.Equals(key))
        {
            player.playerStats.showYellowKey(true);
        }
        if (DoorKeyColor.GREEN.Equals(key))
        {
            player.playerStats.showGreenKey(true);
        }
        if (DoorKeyColor.BLACK.Equals(key))
        {
            player.playerStats.showBlackKey(true);
        }
    }

    public bool containsKey(DoorKeyColor key)
    {
        foreach (var doorKeyColor in collectedKeys)
        {
            Debug.Log("doorKeyColor: " + doorKeyColor);
        }
        
        
        return collectedKeys.Contains(key);
    }
}
