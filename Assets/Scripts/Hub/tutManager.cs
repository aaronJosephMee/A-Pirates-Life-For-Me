using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutManager : MonoBehaviour
{
    private PlayerController _playerController;
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
        if (_playerController != null)
        {
            _playerController.DisablePlayerInput();
        }
        Time.timeScale = 0;
        GameManager.instance.menuOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        Time.timeScale = 1;
        GameManager.instance.menuOpen = false;
        if (_playerController != null)
        {
            _playerController.EnablePlayerInput();
        }
    }
}
