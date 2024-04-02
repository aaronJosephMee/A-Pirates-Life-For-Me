using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        
        _playerController = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
        if (_playerController != null)
        {
            _playerController.DisablePlayerInput();
        }
        Time.timeScale = 0;
        
    }
    void OnDestroy()
    {
        Time.timeScale = 1;
        
        if (_playerController != null)
        {
            _playerController.EnablePlayerInput();
        }
    }
}
