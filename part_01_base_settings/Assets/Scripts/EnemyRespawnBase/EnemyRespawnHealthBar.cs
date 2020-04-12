using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRespawnHealthBar : MonoBehaviour
{
    public Image forgroundImage;
    public void HandleEvent(float pct)
    {
        forgroundImage.GetComponent<Image>().fillAmount = pct;
        //Debug.Log("PlayerHealthBarController - HandleHealthChanged ...");
    }
	
    void Awake()
    {
        //Debug.Log("PlayerHealthBarController ...");
        gameObject.GetComponent<EnemyRespawnHealthController>().OnHealthPctChanged += HandleEvent;
    }
}
