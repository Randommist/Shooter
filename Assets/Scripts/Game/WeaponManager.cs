using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : MonoBehaviour
{
    public UIController MyUiController;
    [SerializeField] private Transform WeaponPosition;
    [SerializeField] private GameObject hands;
    public Weapon currentWeapon;
    public LayerMask WeaponLayer;
    


    void Start()
    {
        hands.SetActive(false);
    }

    void Update()
    {

        if (!GlobalVariables.IsPause)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D colliderUnderMouse = Physics2D.OverlapPoint(mousePosition, WeaponLayer);
            if (currentWeapon == null)
            {
                if (colliderUnderMouse != null)
                {
                    if (colliderUnderMouse.GetComponent<Weapon>() != null)
                    {
                        if (Vector2.Distance((Vector2)transform.position, (Vector2)colliderUnderMouse.transform.position) <= 3f)
                        {
                            if (Input.GetButtonDown("PicUp"))
                            {
                                PicUp(colliderUnderMouse);
                            }
                        }
                    }
                }
            }
            else
            {
                currentWeapon.transform.position = WeaponPosition.position;
                currentWeapon.transform.rotation = WeaponPosition.rotation;
            }

            if (Input.GetButtonDown("Drop"))
            {
                if (currentWeapon != null)
                {
                    Drop();
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (currentWeapon != null)
                {
                    currentWeapon.TriggerPress();
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                if (currentWeapon != null)
                {
                    currentWeapon.TriggerDismiss();
                }
            }

            if (Input.GetButtonDown("Reload"))
            {
                if (currentWeapon != null)
                {
                    if (currentWeapon.CurrentAmmo < currentWeapon.MaxCurrentAmmo)
                    {
                        if (currentWeapon.BagAmmo > 0)
                        {
                            currentWeapon.Reload();
                        }
                    }
                }
            }
        }
    }



    private void PicUp(Collider2D item)
    {
        hands.SetActive(true);
        currentWeapon = item.GetComponent<Weapon>();

        currentWeapon.UIListner = MyUiController;
        MyUiController.UpdateAmmoText();
    }

    private void Drop()
    {
        hands.SetActive(false);
        currentWeapon.AudioSource.Stop();
        currentWeapon.IsReloading = false;
        currentWeapon.UIListner = null;
        currentWeapon.TriggerDismiss();
        currentWeapon = null;
        MyUiController.UpdateAmmoText();
    }


}
