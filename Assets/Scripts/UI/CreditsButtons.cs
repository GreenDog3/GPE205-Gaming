using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtons : MonoBehaviour
{
    public void OpenCreditsMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateCreditsState();
        }
    }
}
