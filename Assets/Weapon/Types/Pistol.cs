using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void TriggerPress()
    {
        base.TriggerPress();
        Shoot(1);
    }
}
