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
    private float shotCounter;
    public Transform firePoint;
    public float damage = 25f;
    private IEnumerator shootBulletCoroutine;

    private void Start()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    public void startShooting(bool fire)
    {
        if (fire && playerController.ammoController.currentAmmo > 0)
        {
            if (!shootingProcessStarted)
            {
                shootBulletCoroutine = shootingBulletProcess(timeBetweenShots);
                StartCoroutine(shootBulletCoroutine);
                shootingProcessStarted = true;
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
        while (playerController.ammoController.currentAmmo > 0)
        {
            BulletController newBullet =
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            newBullet.speed = bulletSpeed;
            newBullet.setDamage(damage);
            newBullet.setPlayerController(playerController);
            playerController.ammoController.changeAmmo(-1);
            yield return new WaitForSeconds(waitTime);
        }
    }
}