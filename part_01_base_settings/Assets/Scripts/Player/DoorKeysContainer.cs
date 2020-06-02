using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeysContainer : MonoBehaviour
{
    public HashSet<DoorKeyColor> collectedKeys = new HashSet<DoorKeyColor>();
    
    public void addDoorKey(DoorKeyColor key)
    {
        collectedKeys.Add(key);
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
