using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    [SerializeField] public float deltaShoot;
    private bool canShoot = true;

    private void Update()
    {
        if (IsTriggerPulled == true)
        {
            if(canShoot == true)
            {
                Shoot(1);
                canShoot = false;
                StartCoroutine(DeltaShoot());
            }
        }


    }

    IEnumerator DeltaShoot()
    {
        yield return new WaitForSeconds(deltaShoot);
        canShoot = true;
    }
}
