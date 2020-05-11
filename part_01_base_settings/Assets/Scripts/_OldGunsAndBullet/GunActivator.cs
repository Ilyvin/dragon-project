using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    AUTOMAT,
    AK74,
    PISTOLET,
    VINTOVKA
}
public class GunActivator : MonoBehaviour
{
    public WeaponType weaponType;
    public bool IS_ACTIVE = false;
    public GunController gunController;
    void Start()
    {
        gunController = gameObject.GetComponent<GunController>();
        if (!IS_ACTIVE)
        {
            gunController.model.SetActive(false);
            gunController.enabled = false;
        }
    }

    public void activateWeapon(bool flag)
    {
        IS_ACTIVE = flag;
        if (gunController == null)
        {
            gunController = gameObject.GetComponent<GunController>();
        }
        gunController.enabled = flag;
        gunController.model.SetActive(flag);
    }
}
