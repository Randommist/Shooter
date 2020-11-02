using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Move : MonoBehaviour
{
    public float Speed = 1;
    private Rigidbody2D rb;
    private Vector2 motion;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 smooth = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 direction = NormalizeDir(smooth);
        motion = new Vector2(direction.x * Mathf.Abs(smooth.x), direction.y * Mathf.Abs(smooth.y)) * Speed;
    }

    void FixedUpdate()
    {
        if (!GlobalVariables.IsPause)
        {
            rb.MovePosition(rb.position + motion * Time.fixedDeltaTime);
        }
    }


    Vector3 NormalizeDir(Vector3 Dir)
    {
        float x = Dir.x;
        float y = Dir.y;
        float z = Dir.z;

        if(x==0 & y==0 & z==0)
        {
            return new Vector3(0, 0, 0);
        }

        float hypotenuse = Mathf.Sqrt( (x * x) + (y * y) + (z * z) );
        x = x / hypotenuse;
        y = y / hypotenuse;
        z = z / hypotenuse;
    
        Vector3 Output = new Vector3(x, y, z);
        return Output;
    }
}
