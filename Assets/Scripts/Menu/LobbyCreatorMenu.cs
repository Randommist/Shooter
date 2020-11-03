using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LobbyCreatorMenu : MonoBehaviour
{
    public NetworkManager NM;

    public void CreateLobby()
    {
        NM.onlineScene = "Game";
        NM.StartHost();
    }
}
