using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectSettings : NetworkBehaviour
{
    public CircleCollider2D col;

    [Header("если isLocal")]
    public bool SetNameLocal = false;
    [Header("если не isLocal")]
    public bool SetRigidbodyKinematic = false;
    public float CollisionScale = 1;

    void Start()
    {
        if (isLocalPlayer && SetNameLocal)
        {
            gameObject.name = "Local";
        }
        else if(SetRigidbodyKinematic)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            col.radius *= CollisionScale;
        }
    }
}
