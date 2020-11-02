using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Rotate : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (!GlobalVariables.IsPause)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            transform.up = direction;
        }
    }
}
