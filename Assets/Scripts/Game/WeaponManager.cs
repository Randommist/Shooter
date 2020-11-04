using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{
    public UIController MyUiController;
    [SerializeField] private Transform WeaponPosition;
    [SerializeField] private HandSwith hands;
    public Weapon currentWeapon;
    public LayerMask WeaponLayer;
    


    void Start()
    {
        hands.Show(false);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

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
                        if (!colliderUnderMouse.GetComponent<Weapon>().isInHands)
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
        currentWeapon = item.GetComponent<Weapon>();
        currentWeapon.isInHands = true;
        currentWeapon.UIListner = MyUiController;
        MyUiController.UpdateAmmoText();


        CmdPicUp(item.GetComponent<NetworkIdentity>());
    }

    [Command]
    void CmdPicUp(NetworkIdentity item)
    {
        currentWeapon = item.GetComponent<Weapon>();
        currentWeapon.isInHands = true;
        hands.Show(true);
        item.AssignClientAuthority(connectionToClient);
    }

    private void Drop()
    {
        currentWeapon.AudioSource.Stop();
        currentWeapon.IsReloading = false;
        currentWeapon.UIListner = null;
        currentWeapon.TriggerDismiss();

        CmdDrop(currentWeapon.GetComponent<NetworkIdentity>());

        currentWeapon = null;
        MyUiController.UpdateAmmoText();
    }

    [Command]
    void CmdDrop(NetworkIdentity item)
    {
        currentWeapon.AudioSource.Stop();
        currentWeapon.IsReloading = false;
        currentWeapon.UIListner = null;
        currentWeapon.TriggerDismiss();
        currentWeapon.isInHands = false;
        currentWeapon = null;
        hands.Show(false);
        item.RemoveClientAuthority();
    }

}
