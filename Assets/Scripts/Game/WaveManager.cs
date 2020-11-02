using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WaveManager : MonoBehaviour
{
    public GameObject[] Zombies;
    public Transform[] SpawnPoints;
    public static int WaveLevel = 0;

    private Sun sun;
    private float spawnTime = 5;
    private float spawnTimeLater;

    private void Start()
    {
        sun = GameObject.Find("Sun").GetComponent<Sun>();
    }

    void Update()
    {
        if (sun.Hours >= 22 || sun.Hours < 8)
        {
            //Спавн зомби
            spawnTimeLater += Time.deltaTime;
            if (spawnTimeLater >= spawnTime)
            {
                Instantiate(Zombies[0], SpawnPoints[0].position, Quaternion.identity);
                spawnTimeLater = 0;
            }
        }
    }
}
