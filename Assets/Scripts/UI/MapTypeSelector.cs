using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapTypeSelector : MonoBehaviour
{
    public void SetMapType(int val)
    {
        if (GameManager.instance != null)
        {
            if (val == 0)
            {
                GameManager.instance.typeOfMap = 0;
            }
            else if (val == 1)
            {
                GameManager.instance.typeOfMap = 1;
            }
            else if (val == 2)
            {
                GameManager.instance.typeOfMap = 2;
            }
        }
    }
}
