using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GunType
{
    AUTOMAT,
    AK74,
    PISTOLET,
    VINTOVKA
}

public class NewGunController : MonoBehaviour
{
    public WeaponType weaponType;
    public bool IS_ACTIVE = false;

    public int magazinLimit = 10; //информация из GunSettings
    public int currentMagazinAmmo = 0; //информация из GunSettings
    public float timeForMagazinReload = 2f; //информация из GunSettings
    private bool isReloading = false;

    public PlayerController playerController;
    //------------------------------------

    public BulletController bulletPrefab; //информация из GunSettings
    public float bulletSpeed = 50f; //информация из GunSettings
    public float timeBetweenShots = 0.5f; //информация из GunSettings

    public bool isArmorPiercing = false; //бронебойный
    public int enemiesPiercingLimit = 1; //количество врагов, которых можно убить подряд насквозь


    public Transform firePoint; //информация из GunSettings
    public float damage = 25f; //информация из GunSettings


    private IEnumerator shootBulletCoroutine;

    public AudioClip shotSound; //информация из GunSettings
    public AudioClip ammoReloadSound; //информация из GunSettings
    public AudioClip noAmmoSound; //информация из GunSettings
    private AudioSource audioSource;

    public GameObject model; //информация из GunSettings
    public Sprite weaponIcon; 
    //--------------------------------

    public bool isSingleShooting = false;
    private float fireRate = 15f;
    private float nextTimeToFire = 0f;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private int i = 0;
    private int j = 0;

    private void FixedUpdate()
    {
        if (IS_ACTIVE)
        {
            if (isReloading)
            {
                return;
            }

            if (currentMagazinAmmo <= 0 && Time.time >= nextTimeToFire)
            {
                if (playerController.ammoController.currentAmmo > 0)
                {
                    Debug.Log("Reload...");
                    nextTimeToFire = Time.time + timeForMagazinReload;
                    StartCoroutine(Reload());
                }
                else
                {
                    if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                    {
                        Shoot();
                    }
                }
            }
            else
            {
                if (isSingleShooting)
                {
                    //это метод для стрельбы из пистолета - одиночными.
                    if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                    {
                        Shoot();
                    }
                }
                else
                {
                    //это метод для стрельбы из автомата очередью.
                    if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                    {
                        Shoot();
                    }
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading Ammo ... start");
        audioSource.clip = ammoReloadSound;
        audioSource.Play();

        int ammoFromAmmoController = playerController.ammoController.getAmmoForMagazin(magazinLimit);
        updateMagazinAmmo(ammoFromAmmoController);

        yield return new WaitForSeconds(timeForMagazinReload);

        Debug.Log("Reloading Ammo ... finish");
        isReloading = false;
    }

    private void updateMagazinAmmo(int deltaAmmo)
    {
        currentMagazinAmmo += deltaAmmo;
        playerController.playerStats.setMagazinAmmoValue(currentMagazinAmmo);
    }

    public void Shoot()
    {
        nextTimeToFire = Time.time + timeBetweenShots;

        if (currentMagazinAmmo == 0 && playerController.ammoController.currentAmmo == 0)
        {
            audioSource.clip = noAmmoSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = shotSound;
            audioSource.Play();

            BulletController newBullet =
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            newBullet.speed = bulletSpeed;
            newBullet.setDamage(damage);
            newBullet.setIsArmorPiercing(isArmorPiercing);
            newBullet.setEnemiesPiercingLimit(enemiesPiercingLimit);
            newBullet.setPlayerController(playerController);

            updateMagazinAmmo(-1);
        }
    }


    public void activateWeapon(bool flag)
    {
        IS_ACTIVE = flag;
        model.SetActive(flag);

        if (flag)
        {
            playerController.playerStats.setMagazinAmmoValue(currentMagazinAmmo);
            playerController.playerStats.setWeaponIcon(weaponIcon);
        }
    }

    public void fillMagazinIfEmpty()
    {
        if (currentMagazinAmmo == 0)
        {
            StartCoroutine(Reload());
            //Debug.Log("Автоматически пополняю магазин.");
        }

        //else - Debug.Log("Автоматически пополнять магазин не надо.");
    }
}