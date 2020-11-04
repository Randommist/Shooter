using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WaveManager : NetworkBehaviour
{
    public GameObject[] Zombies;
    public Transform[] SpawnPoints;
    public static int WaveLevel = 0;
    public Sun Sun;

    private float spawnTime = 5;
    private float spawnTimeLater;

    void Update()
    {
        if (Sun.Hours >= 22 || Sun.Hours < 8)
        {
            //Спавн зомби
            spawnTimeLater += Time.deltaTime;
            if (spawnTimeLater >= spawnTime)
            {
                GameObject e = Instantiate(Zombies[0], SpawnPoints[0].position, Quaternion.identity) as GameObject;
                NetworkServer.Spawn(e);
                spawnTimeLater = 0;
            }
        }
    }
}
