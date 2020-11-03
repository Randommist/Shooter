using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Mirror;

public class ConnectionMenu : MonoBehaviour
{
    public NetworkManager NM;

    public void Connection(InputField inputIP)
    {
        NM.networkAddress = inputIP.text;
        NM.onlineScene = "Game";
        NM.StartClient();
    }
}
