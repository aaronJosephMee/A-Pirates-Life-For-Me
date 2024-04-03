using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public bool buttonClicked = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<Button>().onClick.AddListener(OpenSettings);
    }
            
    // Update is called once per frame
    void Update()
    {

    }

    void OpenSettings()
    {
        if (!buttonClicked)
        {
            Instantiate(GameManager.instance.settingsMenu);
            GameManager.instance.settingsMenu.SetActive(true);
            buttonClicked = true;
        }
    }
}
