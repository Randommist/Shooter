using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int fractionsNumber;
    [SerializeField] private float deltaShoot;
    private bool canShoot = true;

    public override void TriggerPress()
    {
        base.TriggerPress();
        if(canShoot == true)
        {
            Shoot(fractionsNumber);
            canShoot = false;
            StartCoroutine(DeltaShoot());
        }
    }

    IEnumerator DeltaShoot()
    {
        yield return new WaitForSeconds(deltaShoot);
        canShoot = true;
    }

    public override void Reload()
    {
        StartCoroutine(AddAmmo());
    }

    public override void Shoot(int NumberOfFractions)
    {
        IsReloading = false;
        base.Shoot(NumberOfFractions);
    }

}
