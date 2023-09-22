using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class SFXButton : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;

    public void PlaySFX()
    {
        source.PlayOneShot(clip);
    }
}
