using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Weapon : NetworkBehaviour
{
    [Header("Звуки")]
    [SerializeField] private AudioClip soundShoot;
    [SerializeField] private AudioClip soundMisfire;
    [SerializeField] public AudioClip soundReload;
    [HideInInspector] public AudioSource AudioSource;
    [Header("Оружие")]
    [SerializeField] protected string weaponName;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float scatter;
    [SerializeField] protected int maxCurrentAmmo;
    [SerializeField] protected int maxBagAmmo;
    [SerializeField] protected float timeReload;
    [SyncVar]
    [HideInInspector] public int CurrentAmmo;
    [SyncVar]
    [HideInInspector] public int BagAmmo;
    [HideInInspector] public bool IsReloading;
    [HideInInspector] public bool IsTriggerPulled;
    [HideInInspector] public UIController UIListner;

    [SyncVar]
    public bool isInHands = false;

    public int MaxCurrentAmmo
    {
        get { return maxCurrentAmmo; }
    }

    public int MaxBagAmmo
    {
        get { return maxBagAmmo; }
    }

    public float TimeReload
    {
        get { return timeReload;  }
    }

    public virtual void Shoot(int NumberOfFractions)
    {
        if (!IsReloading)
        {
            if (CurrentAmmo > 0)
            {
                AudioSource.pitch = Random.Range(0.95f, 1.05f);
                AudioSource.PlayOneShot(soundShoot);
                CurrentAmmo--;
                for(int i = 0; i < NumberOfFractions; i++)
                {
                    Quaternion bulletDirection = Quaternion.Euler(0, 0, Random.Range(-scatter, scatter));                 
                    Instantiate(bullet, transform.position, transform.rotation * bulletDirection);
                }

                if (UIListner != null)
                {
                    UIListner.UpdateAmmoText();
                }
            }
        }
    }

    public virtual void Reload()
    {
        StartCoroutine(ReplacementMagazine());
    }

    public IEnumerator ReplacementMagazine()
    {
        IsReloading = true;
        AudioSource.clip = soundReload;
        AudioSource.Play();
        yield return new WaitForSeconds( TimeReload);
        if (IsReloading)
        {
            IsReloading = false;
            if ((BagAmmo + CurrentAmmo) > MaxCurrentAmmo)
            {
                BagAmmo -= MaxCurrentAmmo - CurrentAmmo;
                CurrentAmmo = MaxCurrentAmmo;
            }
            else
            {
                CurrentAmmo += BagAmmo;
                BagAmmo = 0;
            }
            UIListner.UpdateAmmoText();
        }
    }

    public IEnumerator AddAmmo()
    {
        while (CurrentAmmo < maxCurrentAmmo)
        {
            IsReloading = true;
            AudioSource.clip = soundReload;
            AudioSource.Play();
            yield return new WaitForSeconds(TimeReload);
            if (IsReloading)
            {
                IsReloading = false;
                if (BagAmmo >= 1)
                {
                    CurrentAmmo++;
                    BagAmmo--;
                    UIListner.UpdateAmmoText();
                }
                else
                {
                    break;
                }

            }
            else
            {
                break;
            }
        }

    }

    public virtual void TriggerPress()
    {
        IsTriggerPulled = true;
        if (CurrentAmmo <= 0 && !IsReloading)
        {
            AudioSource.PlayOneShot(soundMisfire);
        }
    }

    public virtual void TriggerDismiss()
    {
        IsTriggerPulled = false;
    }

    public virtual void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        CurrentAmmo = MaxCurrentAmmo;
        BagAmmo = MaxBagAmmo;
    }



}
