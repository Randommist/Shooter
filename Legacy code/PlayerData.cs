using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class PlayerData : MonoBehaviour
{
    public Socket Stream;
    public string Name;
    public Vector2 Position;

    /*public PlayerData(string name, Socket stream)
    {
        Name = name;
        Stream = stream;
    }*/
}
