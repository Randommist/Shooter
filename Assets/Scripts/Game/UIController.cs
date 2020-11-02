using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text clock;
    [SerializeField] private Text currentAmmoText;
    [SerializeField] private Text bagAmmoText;
    [SerializeField] private Image ReloadImage;
    [SerializeField] private WeaponManager Player;

    private float passedTimeReload;

    private Sun sun;

    void Update()
    {
        //Оружие
        if(Player != null)
        {
            if (Player.currentWeapon != null)
            {
                if (Player.currentWeapon.IsReloading)
                {
                    passedTimeReload += Time.deltaTime;
                    float countTime = passedTimeReload / Player.currentWeapon.TimeReload;
                    ReloadImage.fillAmount = countTime;
                    if (passedTimeReload >= Player.currentWeapon.TimeReload)
                    {
                        passedTimeReload = 0;
                    }
                }
                else
                {
                    passedTimeReload = 0;
                    float countAmmo = (float)Player.currentWeapon.CurrentAmmo / (float)Player.currentWeapon.MaxCurrentAmmo;
                    ReloadImage.fillAmount = countAmmo;
                }

            }
        }
        else
        {
            GameObject p = GameObject.Find("Local");
            if (p != null)
            {
                Player = p.GetComponent<WeaponManager>();
                Player.MyUiController = gameObject.GetComponent<UIController>();
            }
        }
        //Чвсы
        if(sun != null)
        {
            string min;
            if (sun.Minutes < 10)
            {
                min = "0" + ((int)sun.Minutes).ToString();
                clock.text = sun.Hours + ":" + min;
            }
            else
            {
                clock.text = sun.Hours + ":" + (int)sun.Minutes;
            }
        }
        else
        {
            GameObject s = GameObject.Find("Sun");
            if (s != null)
                sun = s.GetComponent<Sun>();
        }


    }

    public void UpdateAmmoText()
    {
        if(Player.currentWeapon != null)
        {
            currentAmmoText.text = Player.currentWeapon.CurrentAmmo.ToString();
            bagAmmoText.text = Player.currentWeapon.BagAmmo.ToString();
        }
        else
        {
            currentAmmoText.text = "0";
            bagAmmoText.text = "0";
            ReloadImage.fillAmount = 0;
            passedTimeReload = 0;
        }
        
    }
}
