using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void SinglePlay()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
