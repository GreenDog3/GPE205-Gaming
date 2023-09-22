using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    void Start()
    {
        OnSFXVolumeChange();
    }

    public void OnSFXVolumeChange()
    {
        //start with default slider value
        float newVolume = volumeSlider.value;
        if (newVolume <= 0)
        {
            //if the slider's at 0, set to min volume
            newVolume = -80;
        }
        else
        {
            //find log10 value
            newVolume = Mathf.Log10(newVolume);
            //make it in the correct range
            newVolume = newVolume * 20;
        }
        //set the volume
        mixer.SetFloat("SFXVolume", newVolume);
        
    }
}
