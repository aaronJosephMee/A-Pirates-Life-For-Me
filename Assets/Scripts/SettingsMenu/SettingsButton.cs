using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public SceneName scene;
    void Start()
    {
        GameManager.instance.menuOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.GetComponent<Button>().onClick.AddListener(OpenSettings);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenSettings()
    {
        GameManager.instance.menuOpen = false;
        
    }
}
