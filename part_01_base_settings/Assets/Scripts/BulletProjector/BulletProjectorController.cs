using UnityEngine;
using System.Collections;

public class BulletProjectorController : MonoBehaviour {
	
    public float lifetime = 5f;

    void Start() 
    {
        Destroy(gameObject, lifetime);
    }
}