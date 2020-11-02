using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public List<Canvas> Menus;

    public void SetMenu(int num)
    {
        for (int i = 0; i < Menus.Count; i++)
        {
            if (i == num)
                Menus[i].enabled = true;
            else
                Menus[i].enabled = false;
        }
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

}
