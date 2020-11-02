using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Canvas Menu;

    private void Start()
    {
        Menu.enabled = GlobalVariables.IsPause;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.enabled = !Menu.enabled;
            GlobalVariables.IsPause = Menu.enabled;

            if (GlobalVariables.IsPause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void Continue()
    {

    }

    public void Options()
    {

    }

    public void Exit()
    {

    }
}
