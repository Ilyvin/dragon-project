using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public Transform player;
    public bool staticMap = false;
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = newPosition;

        if (!staticMap)
        {
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}
