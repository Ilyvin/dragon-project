using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperienceController : MonoBehaviour
{
    public int currentExpa;
    
    // Update is called once per frame
    public void updateExpa(int delta)
    {
        currentExpa += delta;
    }
}
