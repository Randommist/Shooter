using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnWeapons : MonoBehaviour
{
    public GameObject[] Weapons;
    public Transform[] SpawnPoints;

    private void Start()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Instantiate(Weapons[Random.Range(0, Weapons.Length)], SpawnPoints[i].position, Quaternion.identity);
        }

    }
}
