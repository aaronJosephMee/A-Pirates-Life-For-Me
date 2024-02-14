using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerController.DisablePlayerInput();
        Time.timeScale = 0;
        GameManager.instance.menuOpen = true;
    }
    void OnDestroy()
    {
        Time.timeScale = 1;
        GameManager.instance.menuOpen = false;
        _playerController.EnablePlayerInput();
    }
}
