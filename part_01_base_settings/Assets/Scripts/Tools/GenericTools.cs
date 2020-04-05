using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTools : MonoBehaviour
{
    private IEnumerator waitForSecondsCoroutine;
    
    

    private IEnumerator waitForSeconds(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
        }
    }
}
