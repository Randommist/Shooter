using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public NetworkManager NM;

    public void SinglePlay()
    {
        NM.onlineScene = "Game";
        NM.StartHost();
    }
}
