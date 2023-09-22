using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapSeedChanger : MonoBehaviour
{
    public void SetMapSeed(string val)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.mapSeed = int.Parse(val);
        }
    }
}
