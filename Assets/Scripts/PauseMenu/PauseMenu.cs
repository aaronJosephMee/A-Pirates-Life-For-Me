using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    private GameObject _playerController;
    // Start is called before the first frame update
    void Start()
    {
        
        _playerController = GameObject.FindGameObjectWithTag("Player");
        if (_playerController != null)
        {
            //_playerController.GetComponent<PlayerController>().DisablePlayerInput();
            if (_playerController.GetComponent<CharacterAiming>()){
                _playerController.GetComponent<CharacterAiming>().enabled = false;
            }
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        
    }
    void OnDestroy()
    {
        Time.timeScale = 1;
        
        if (_playerController != null)
        {
            //_playerController.GetComponent<PlayerController>().EnablePlayerInput();
            if (_playerController.GetComponent<CharacterAiming>()){
                _playerController.GetComponent<CharacterAiming>().enabled = true;
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
