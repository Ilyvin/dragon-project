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
    
    public GameObject redKeyIcon;
    public GameObject greenKeyIcon;
    public GameObject blueKeyIcon;
    public GameObject yellowKeyIcon;
    public GameObject blackKeyIcon;

    private void Start()
    {
        bronikIcon.SetActive(false);
        
        redKeyIcon.SetActive(false);
        greenKeyIcon.SetActive(false);
        blueKeyIcon.SetActive(false);
        yellowKeyIcon.SetActive(false);
        blackKeyIcon.SetActive(false);
    }

    public void showRedKey(bool flag)
    {
        redKeyIcon.SetActive(flag);
    }
    
    public void showGreenKey(bool flag)
    {
        greenKeyIcon.SetActive(flag);
    }
    
    public void showBlueKey(bool flag)
    {
        blueKeyIcon.SetActive(flag);
    }
    
    public void showYellowKey(bool flag)
    {
        yellowKeyIcon.SetActive(flag);
    }
    
    public void showBlackKey(bool flag)
    {
        blackKeyIcon.SetActive(flag);
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