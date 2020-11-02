using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSaver : MonoBehaviour
{
    private void Start()
    {
        GetComponent<InputField>().text = PlayerPrefs.GetString("PlayerName");
    }

    public void SaveName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

}
