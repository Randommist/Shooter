using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Type bulletType = Type.Standart;
    [SerializeField] private float damage = 1;
    [SerializeField] private float force = 1;
    [SerializeField] private float timeToDead = 1;
    private float leftTimeToDead;
    private Rigidbody2D rb;

    public enum Type
    {
        Standart, Burning
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.up * force, ForceMode2D.Impulse);
        leftTimeToDead = timeToDead;
    }


    void Update()
    {
        leftTimeToDead -= Time.deltaTime;
        if (leftTimeToDead<=0)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<IDamage>() != null)
        {
            collision.gameObject.GetComponent<IDamage>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
