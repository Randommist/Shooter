using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Zombie : MonoBehaviour , IDamage
{
    public Transform Target;
    public List<Transform> Humans;
    public float MaxSpeed;
    protected float _speedDivider = 1;
    [SerializeField] private float _health = 100;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackSpeed;
    protected Rigidbody2D rb;

    public enum ZombieType
    {
        Normal
    }

    public void TakeDamage(float Damage)
    {
        StartCoroutine(ShowEffect());
        _health -= Damage;
        if(_health <= 0)
        {
            //GameObject.Find("NetworkManager").GetComponent<Host>
            Death();
        }
    }

    IEnumerator ShowEffect()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected void Death()
    {
        Destroy(gameObject);
    }

    protected virtual Transform FindPriority()
    {
        if(Humans.Count > 0)
        {
            Transform minDistance = Humans[0];
            foreach (Transform human in Humans)
            {
                if (Vector2.Distance(transform.position, human.position) < Vector2.Distance(transform.position, minDistance.position))
                {
                    minDistance = human;
                }
            }

            return minDistance;
        }
        return null;
    }

    protected void AddHuman(Collider2D other)
    {
        bool nameTaken = false;
        foreach (Transform human in Humans)
        {
            if (human.name == other.name)
                nameTaken = true;
        }
        if (!nameTaken)
            Humans.Add(other.transform);
    }

    protected void RemoveHuman(Collider2D other)
    {
        int numRemove = -1;
        for (int i = 0; i < Humans.Count; i++)
        {
            if (other.name == Humans[i].name)
                numRemove = i;
        }
        if (numRemove >= 0)
            Humans.RemoveAt(numRemove);
    }

    protected void LookAt2D(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}