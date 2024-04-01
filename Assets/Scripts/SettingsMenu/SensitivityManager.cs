using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    public static SensitivityManager Instance { get; private set; }

    public Slider sensitivitySlider;

    public float mouseSensitivity = 1.0f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadSettings();
    }

    public void OnSensitivityChanged(float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
        SaveSettings();
        Debug.Log("Sensitivity changed to " +  newSensitivity);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        PlayerPrefs.Save();
        sensitivitySlider.value = Instance.mouseSensitivity;
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            sensitivitySlider.value = Instance.mouseSensitivity;
            Debug.Log(mouseSensitivity);
            Debug.Log(sensitivitySlider.value);
        }
    }
}
