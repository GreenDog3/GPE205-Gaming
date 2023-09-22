using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplayerSwitch : MonoBehaviour
{
    public void SetMultiplayer(bool is2P)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.is2PGame = is2P; return;
        }
    }
}
