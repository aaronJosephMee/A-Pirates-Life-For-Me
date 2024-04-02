using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sFXSlider;

    private void Start()
    {
        // Retrieves the previous setting on the mixer
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(musicSlider.value);
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            SetSFXVolume(sFXSlider.value);
        }
        
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Method to set the volume of music
    private void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    // Method to set the volume of sound effects
    private void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
