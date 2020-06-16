using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponContoller : MonoBehaviour
{
    public int selectedWeapon = 0;
    public HashSet<String> weaponTypes = new HashSet<String>();
    public int globalWeaponCounter;

    void Start()
    {
        initWeaponTypes();
        SelectWeapon();
        globalWeaponCounter = 1;
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //Debug.Log("Input.Mouse ScrollWheel > 0f, selectedWeapon=" + selectedWeapon);
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            //Debug.Log("Input.Mouse ScrollWheel < 0f, selectedWeapon=" + selectedWeapon);
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    private void SelectWeaponByType(String weaponType)
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (weaponType.Equals(weapon.gameObject.GetComponent<NewGunController>().weaponType.ToString()))
            {
                weapon.gameObject.GetComponent<NewGunController>().activateWeapon(true);
            }
            else
            {
                weapon.gameObject.GetComponent<NewGunController>().activateWeapon(false);
            }
        }
    }

    private void SelectWeapon()
    {
        //Debug.Log("SelectWeapon()");
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.GetComponent<NewGunController>().activateWeapon(true);
            }
            else
            {
                weapon.gameObject.GetComponent<NewGunController>().activateWeapon(false);
            }

            i++;
        }
    }

    private void initWeaponTypes()
    {
        foreach (Transform weapon in transform)
        {
            weaponTypes.Add(weapon.gameObject.GetComponent<NewGunController>().weaponType.ToString());
        }
    }

    public void addNewWeapon(GameObject weaponPrefab)
    {
        //Debug.Log("addNewWeapon");

        if (weaponPrefab != null && weaponPrefab.gameObject.GetComponent<NewGunController>() != null)
        {
            String wt = weaponPrefab.gameObject.GetComponent<NewGunController>().weaponType.ToString();

            /*foreach (String elem in weaponTypes)
            {
                Debug.Log("elem = " + elem);
            }*/

            if (!weaponTypes.Contains(wt))
            {
                GameObject newWeapon = Instantiate(weaponPrefab, transform.position, transform.rotation);
                newWeapon.transform.parent = transform;
                weaponTypes.Add(wt);
            }
            
            //выбрать новое оружие как основное
            SelectWeaponByType(wt);
        }
    }
}