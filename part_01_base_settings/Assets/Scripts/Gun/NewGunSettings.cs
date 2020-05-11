using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGunSettings : MonoBehaviour
{
    public BulletController bulletPrefab;
    public float bulletSpeed = 50f;
    public float timeBetweenShots = 0.5f;
    public bool isArmorPiercing = false; //бронебойный
    public int enemiesPiercingLimit = 1; //количество врагов, которых можно убить подряд насквозь

    public int magazinLimit = 10;
    public int currentMagazinAmmo = 0;
    public bool magazinFilled = false;
    public float magazinFillDelay = 2f;
    
    public Transform firePoint;
    public float damage = 25f;
    
    public AudioClip shotSound;
    public AudioClip ammoReloadSound;
    public AudioClip noAmmoSound;

    
    

}
