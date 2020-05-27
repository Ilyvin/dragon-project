using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text bronikValue;
    public Text hpValue;
    public Text ammoValue;
    public Text magazinAmmoValue;
    public Text expaValue;
    public Text userMessage;
    public Image weaponIcon;
    public GameObject bronikIcon;

    private void Start()
    {
        bronikIcon.SetActive(false);
    }

    public void setMagazinAmmoValue(int value)
    {
        magazinAmmoValue.GetComponent<Text>().text = value.ToString();
    }

    public void setAmmoValue(int value)
    {
        ammoValue.GetComponent<Text>().text = value.ToString();
    }

    public void setExpaValue(int value)
    {
        expaValue.GetComponent<Text>().text = value.ToString();
    }

    public void setWeaponIcon(Sprite icon)
    {
        weaponIcon.GetComponent<Image>().sprite = icon;
    }
    
    public void showBronik(bool flag)
    {
        bronikIcon.SetActive(flag);
    }

    public void setUserMessage(String msg)
    {
        userMessage.GetComponent<Text>().text = msg;
    }

    public void setHealthValue(int value)
    {
        hpValue.GetComponent<Text>().text = value.ToString();
    }
    
    public void setBronikValue(int value)
    {
        bronikValue.GetComponent<Text>().text = value.ToString();
    }
}