using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void OpenMainMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMainMenuState();
        }

    }
}
