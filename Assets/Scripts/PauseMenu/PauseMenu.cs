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
            Debug.Log(_playerController.name);
            //_playerController.GetComponent<PlayerController>().DisablePlayerInput();
            _playerController.GetComponent<CharacterAiming>().enabled = false;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        GameManager.instance.menuOpen = true;
    }
    void OnDestroy()
    {
        Time.timeScale = 1;
        GameManager.instance.menuOpen = false;
        if (_playerController != null)
        {
            //_playerController.GetComponent<PlayerController>().EnablePlayerInput();
            _playerController.GetComponent<CharacterAiming>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
