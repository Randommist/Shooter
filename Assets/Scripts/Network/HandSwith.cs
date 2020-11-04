using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwith : NetworkBehaviour
{
    [SyncVar]
    public bool isShow = false;
    public SpriteRenderer hands;

    public void Show(bool show)
    {
        isShow = show;
    }

    private void Update()
    {
        hands.enabled = isShow;
    }
}
