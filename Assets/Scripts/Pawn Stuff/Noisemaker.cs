using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour
{
    public float noiseDistance;

    public void MakeNoise(float volume)
    {
        noiseDistance = volume;
    }
}
