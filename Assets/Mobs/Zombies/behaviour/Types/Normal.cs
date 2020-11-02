using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Normal : Zombie
{
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Target = FindPriority();
        if (Target != null)
        {
            LookAt2D(Target);
        }
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            rb.MovePosition(transform.position + transform.up * MaxSpeed / _speedDivider * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Human")
        {
            AddHuman(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Human")
        {
            RemoveHuman(other);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Human")
        {
            _speedDivider = 4;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Human")
        {
            _speedDivider = 1;
        }
    }

}