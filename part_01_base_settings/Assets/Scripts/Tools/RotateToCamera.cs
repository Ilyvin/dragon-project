using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(new Vector3(0,0,-1000));
        transform.Rotate(0, 0, 0);
    }
}
