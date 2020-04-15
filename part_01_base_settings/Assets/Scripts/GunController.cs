using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private bool isFiring;
    private bool shootingProcessStarted = false;
    private PlayerController playerController;
    public BulletController bulletPrefab;
    public float bulletSpeed = 50f;
    public float timeBetweenShots = 0.5f;

    public int magazinLimit = 10;
    public int currentMagazinAmmo = 0;
    public bool magazinFilled = false;
    public float magazinFillDelay = 2f;
    private int bulletCounter;

    public Transform firePoint;
    public float damage = 25f;
    private IEnumerator shootBulletCoroutine;

    public AudioClip shotSound;
    public AudioClip ammoReloadSound;
    public AudioClip noAmmoSound;
    private AudioSource audioSource;

    private void Start()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = shotSound;

        fillMagazin();
    }

    public void fillMagazin()
    {
        magazinFilled = false;
        if (shootBulletCoroutine != null)
        {
            StopCoroutine(shootBulletCoroutine);
        }

        shootingProcessStarted = false;
        //Debug.Log("Line 41");

        if (currentMagazinAmmo < magazinLimit)
        {
            //Debug.Log("Line 45");
            int currentAmmo = playerController.ammoController.getCurrentAmmo();
            if (currentAmmo > 0)
            {
                //Debug.Log("Line 49");

                int requiredAmmo = magazinLimit - currentMagazinAmmo;

                if (currentAmmo > requiredAmmo)
                {
                    currentMagazinAmmo = magazinLimit;
                    playerController.ammoController.changeAmmo(-magazinLimit);
                }
                else
                {
                    currentMagazinAmmo = currentAmmo;
                    playerController.ammoController.changeAmmo(-currentAmmo);
                }

                //Debug.Log("Line 64");

                audioSource.clip = ammoReloadSound;
                audioSource.Play();
                //Debug.Log("Line 68");

                Invoke("setMagazinFilled", magazinFillDelay);
            }
            else
            {
                playerController.playerStats.setUserMessage("Чувак, патроны закончились");
                //Debug.Log("Нет патронов.");
            }
        }
        else
        {
            //Debug.Log("Магазин полон.");
        }
    }

    public void fillMagazinIfEmpty()
    {
        if (currentMagazinAmmo == 0)
        {
            fillMagazin();
            //Debug.Log("Автоматически пополняю магазин.");
        }
        else
        {
            //Debug.Log("Автоматически пополнять магазин не надо.");
        }
    }

    private void setMagazinFilled()
    {
        //Debug.Log("Line 99 setMagazinFilled");
        magazinFilled = true;
    }

    public void changeMagazinAmmo(int ammoDelta)
    {
        //Debug.Log("Line 105 changeMagazinAmmo");

        int result = currentMagazinAmmo + ammoDelta;

        if (result <= 0)
        {
            currentMagazinAmmo = 0;
            //Debug.Log("Пора перезарядить магазин");
            fillMagazin();
        }
        else if (result > magazinLimit)
        {
            currentMagazinAmmo = magazinLimit;
        }
        else
        {
            currentMagazinAmmo = result;
        }
    }

    public void startShooting(bool fire)
    {
        if (fire)
        {
            if (magazinFilled)
            {
                //Debug.Log("Line 131 if (magazinFilled)");

                if (currentMagazinAmmo > 0)
                {
                    if (!shootingProcessStarted)
                    {
                        //Debug.Log("Line 137 StartCoroutine(shootBulletCoroutine);");
                        shootBulletCoroutine = shootingBulletProcess(timeBetweenShots);
                        StartCoroutine(shootBulletCoroutine);
                        shootingProcessStarted = true;
                    }
                }

                /*else
                {
                    Debug.Log("-----Патронов нет!");
                    audioSource.clip = noAmmoSound;
                    audioSource.Play();
                }*/
            }
            else
            {
                //Debug.Log("Line 151 else if (magazinFilled)");
                //Debug.Log("Line 137 StopCoroutine(shootBulletCoroutine);");
            }

            if (playerController.ammoController.currentAmmo + currentMagazinAmmo == 0)
            {
                //Debug.Log("-----Патронов нет!");
                audioSource.clip = noAmmoSound;
                audioSource.Play();
            }
        }
        else
        {
            if (shootBulletCoroutine != null)
            {
                StopCoroutine(shootBulletCoroutine);
            }

            shootingProcessStarted = false;
        }
    }

    private IEnumerator shootingBulletProcess(float waitTime)
    {
        //Debug.Log("Line 178 shootingBulletProcess");
        while (currentMagazinAmmo > 0)
        {
            //Debug.Log("Line 181 while (currentMagazinAmmo > 0)");
            audioSource.clip = shotSound;
            audioSource.Play();
            BulletController newBullet =
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            newBullet.speed = bulletSpeed;
            newBullet.setDamage(damage);
            newBullet.setPlayerController(playerController);
            //playerController.ammoController.changeAmmo(-1);
            //Debug.Log("Line 190 before changeMagazinAmmo(-1);");
            changeMagazinAmmo(-1);
            //Debug.Log("Line 192 after changeMagazinAmmo(-1);");
            yield return new WaitForSeconds(waitTime);
        }
    }
}