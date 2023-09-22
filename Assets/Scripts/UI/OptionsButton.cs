using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public void OpenOptionsMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateOptionsState();
        }
    }
}
