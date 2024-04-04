using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMenu : MonoBehaviour
{
    private SettingsButton settingsButton;
    public bool isPauseMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        settingsButton = FindObjectOfType<SettingsButton>();
        if (settingsButton == null)
        {
            Debug.LogError("SettingsButton not found in the scene");
        }
        this.GetComponent<Button>().onClick.AddListener(CloseSettingsMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseSettingsMenu()
    {
        if (settingsButton != null)
        {
            settingsButton.buttonClicked = false;
        }
        if (isPauseMenu){
            GameManager.instance.menuOpen = false;
        }
        Destroy(this.transform.parent.gameObject);
    }
}
