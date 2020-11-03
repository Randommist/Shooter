using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNameInHierarchy : NetworkBehaviour
{

    void Start()
    {
        if (isLocalPlayer)
        {
            gameObject.name = "Local";
        }
        else
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

}
