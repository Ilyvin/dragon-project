using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponContoller : MonoBehaviour
{
    public int selectedWeapon = 0;
    public HashSet<String> weaponTypes = new HashSet<String>();

    void Start()
    {
        initWeaponTypes();
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Debug.Log("Input.Mouse ScrollWheel > 0f, selectedWeapon=" + selectedWeapon);
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
            Debug.Log("Input.Mouse ScrollWheel < 0f, selectedWeapon=" + selectedWeapon);
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

    private void SelectWeapon()
    {
        Debug.Log("SelectWeapon()");
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
        Debug.Log("addNewWeapon");
        
        if (weaponPrefab != null && weaponPrefab.gameObject.GetComponent<NewGunController>() != null)
        {
            String wt = weaponPrefab.gameObject.GetComponent<NewGunController>().weaponType.ToString();

            foreach (String elem in weaponTypes)
            {
                Debug.Log("elem = " + elem);
            }
            
            if (!weaponTypes.Contains(wt))
            {
                GameObject newWeapon = Instantiate(weaponPrefab, transform.position, transform.rotation);
                newWeapon.transform.parent = transform;
                weaponTypes.Add(wt);

                SelectWeapon();
            }
        }
    }

    /*public PlayerController playerController;
    public WeaponType activeWeaponType = WeaponType.VINTOVKA;
    public Dictionary<WeaponType, GunActivator> weaponDictionary;
    //public GunController actualGunController;
    public GunActivator actualGunActivator;
    public Queue<WeaponType> weaponQueue;
    
    public void initWeaponList()
    {
        weaponDictionary = new Dictionary<WeaponType, GunActivator>();
        weaponQueue = new Queue<WeaponType>();
        
        Debug.Log("[ChangeWeaponContoller] initWeaponList()");
        
        GunActivator[] weaponArray = playerController.gameObject.GetComponentsInChildren<GunActivator>();

        for (int i = 0; i < weaponArray.Length; i++)
        {
            weaponDictionary.Add(weaponArray[i].weaponType, weaponArray[i]);
            weaponArray[i].activateWeapon(false);
            Debug.Log("[ChangeWeaponContoller] weaponList: [" + i + "].weaponType = " + weaponArray[i].weaponType);
            Debug.Log("[ChangeWeaponContoller] weaponList: [" + i + "] = " + weaponArray[i]);
            
            weaponQueue.Enqueue(weaponArray[i].weaponType);
        }

        getInitialGun();
        
        //if (weaponDictionary.ContainsKey(activeWeaponType))
        //{
        //    GunActivator currentWeapon = weaponDictionary[activeWeaponType];
        //    Debug.Log("[ChangeWeaponContoller] currentWeapon != null :" + currentWeapon);
        //    currentWeapon.activateWeapon(true);
        //    actualGunActivator = currentWeapon;
        //}
        //else
        //{
        //    Debug.Log("[ChangeWeaponContoller] currentWeapon == null");
        //}
    }

    public GunController getActualGunController()
    {
        Debug.Log("[ChangeWeaponContoller] getActualGunController()");
        return actualGunActivator.gunController;
    }
    
    public void getInitialGun()
    {
        Debug.Log("[ChangeWeaponContoller] getNextGun");
        WeaponType firstWeaponType = weaponQueue.Dequeue();
           
        if (weaponDictionary.ContainsKey(firstWeaponType))
        {
            Debug.Log("[ChangeWeaponContoller] weaponDictionary.ContainsKey = " + firstWeaponType);
            GunActivator currentWeapon = weaponDictionary[firstWeaponType];
            currentWeapon.activateWeapon(true);
            actualGunActivator = currentWeapon;
        }
        
        weaponQueue.Enqueue(firstWeaponType);
    }

    public void getNextGun()
    {
        Debug.Log("***********getNextGun");
        
        WeaponType[] types = weaponQueue.ToArray();
        foreach (WeaponType type in types)
        {
            Debug.Log("[ChangeWeaponContoller] weaponQueue: " + type.ToString());
        }

        
        Debug.Log("[ChangeWeaponContoller] getNextGun");
        WeaponType firstWeaponType = weaponQueue.Dequeue();
           
        if (weaponDictionary.ContainsKey(firstWeaponType))
        {
            actualGunActivator.activateWeapon(false);
            Debug.Log("[ChangeWeaponContoller] weaponDictionary.ContainsKey = " + firstWeaponType);
            GunActivator currentWeapon = weaponDictionary[firstWeaponType];
            currentWeapon.activateWeapon(true);
            actualGunActivator = currentWeapon;
        }
        
        
        weaponQueue.Enqueue(firstWeaponType);
    }*/
}