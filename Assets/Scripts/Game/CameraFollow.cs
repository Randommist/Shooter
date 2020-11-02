using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;
    public float Speed = 1;

    void FixedUpdate()
    {
        if(Player != null)
        {
            Vector2 pos = Vector2.Lerp(transform.position, Player.transform.position, Speed * Time.fixedDeltaTime);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
        else
        {
            Player = GameObject.Find("Local");
        }

    }
}
