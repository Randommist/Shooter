using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwith : NetworkBehaviour
{
    [SyncVar]
    public bool isShow = false;

    private SpriteRenderer renderer;

    public void Show(bool show)
    {
        isShow = show;
    }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        renderer.enabled = isShow;
    }
}
