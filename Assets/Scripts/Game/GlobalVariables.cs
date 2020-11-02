using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GlobalVariables : MonoBehaviour
{
    public static GameModes PlayMode = GameModes.Host;
    public static bool IsPause;

    public enum GameModes
    {
        Host, Client
    }
}
