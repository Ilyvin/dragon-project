using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType
{
    AUTOMAT,
    AK74,
    PISTOLET,
    VINTOVKA,
    SHOTGUN,
    BAZUKA,
    MACHINEGUN
}
public enum BulletType
{
    BAZUKA_ROCKET,
    BULLET
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
    public int damage = 25; //информация из GunSettings


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

    public float courseDeviationRandomRange = 0.1f;
    public float speedDeviationRandomRange = 0.1f;
    public int shotgunBulletNumber = 10;

    void Awake()
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
                    //Debug.Log("Reload...");
                    nextTimeToFire = Time.time + timeForMagazinReload;
                    StartCoroutine(Reload());
                }
                else
                {
                    if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                    {
                        Shoot(1);
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
                        Shoot(1);
                    }
                }
                else
                {
                    //это метод для стрельбы из автомата очередью.
                    if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                    {
                        Shoot(1);
                    }
                    
                    //это метод для двойного выстрела.
                    if (Input.GetButton("Fire2") && Time.time >= nextTimeToFire)
                    {
                        Shoot(2);
                    }
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        //Debug.Log("Reloading Ammo ... start");
        audioSource.clip = ammoReloadSound;
        audioSource.Play();

        int ammoFromAmmoController = playerController.ammoController.getAmmoForMagazin(magazinLimit);
        updateMagazinAmmo(ammoFromAmmoController);

        yield return new WaitForSeconds(timeForMagazinReload);

        //Debug.Log("Reloading Ammo ... finish");
        isReloading = false;
    }

    private void updateMagazinAmmo(int deltaAmmo)
    {
        currentMagazinAmmo += deltaAmmo;
        playerController.playerStats.setMagazinAmmoValue(currentMagazinAmmo);
    }
    
    public void Shoot(int BulletsNumber)
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

            if (weaponType == WeaponType.SHOTGUN)
            {
                for (int j = 0; j < BulletsNumber; j++)
                {
                    for (int i = 0; i < shotgunBulletNumber; i++)
                    {
                        Quaternion rotation = new Quaternion(
                            firePoint.rotation.x +
                            Random.Range(-courseDeviationRandomRange, courseDeviationRandomRange),
                            firePoint.rotation.y +
                            Random.Range(-courseDeviationRandomRange, courseDeviationRandomRange),
                            firePoint.rotation.z +
                            Random.Range(-courseDeviationRandomRange, courseDeviationRandomRange),
                            firePoint.rotation.w);
                        BulletController newBullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                        /*Debug.Log("Shotgun bullet rotation: " + firePoint.rotation.ToString());
                        Debug.Log("Shotgun bullet rotation: x=" + firePoint.rotation.x + ", y=" + firePoint.rotation.y + ", z=" + firePoint.rotation.z + ", w=" + firePoint.rotation.w);
    
                        Debug.Log("Shotgun bullet rotation with rand: " +
                                  "x=" + firePoint.rotation.x + Random.Range(-courseDeviationRandomRange, courseDeviationRandomRange) + ", " +
                                  "y=" + firePoint.rotation.y + Random.Range(-courseDeviationRandomRange, courseDeviationRandomRange) + ", " +
                                  "z=" + firePoint.rotation.z + Random.Range(-courseDeviationRandomRange, courseDeviationRandomRange) + ", " +
                                  "w=" + firePoint.rotation.w);*/

                        newBullet.speed = bulletSpeed;
                        newBullet.setDamage(damage);
                        newBullet.setIsArmorPiercing(isArmorPiercing);
                        newBullet.setEnemiesPiercingLimit(enemiesPiercingLimit);
                        newBullet.setPlayerController(playerController);
                    }
                }
            }
            else if (weaponType == WeaponType.BAZUKA)
            {
                BulletController newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                newBullet.setBulletType(BulletType.BAZUKA_ROCKET);
                newBullet.speed = bulletSpeed;
                newBullet.setDamage(damage);
                newBullet.setIsArmorPiercing(isArmorPiercing);
                newBullet.setEnemiesPiercingLimit(enemiesPiercingLimit);
                newBullet.setPlayerController(playerController);
            }
            else
            {
                BulletController newBullet =
                    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                newBullet.speed = bulletSpeed;
                newBullet.setDamage(damage);
                newBullet.setIsArmorPiercing(isArmorPiercing);
                newBullet.setEnemiesPiercingLimit(enemiesPiercingLimit);
                newBullet.setPlayerController(playerController);
            }

            updateMagazinAmmo(-BulletsNumber);
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