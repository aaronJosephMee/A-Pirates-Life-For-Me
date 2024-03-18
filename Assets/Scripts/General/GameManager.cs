using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Choices choices;
    private static Vector3 _lastPosition;
    public bool menuOpen;
    private bool movePlayerOnLoad = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Awake(){
        if (instance == null)
        {
            instance = this;
            InitializeChoices();
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string scene, bool restorePosition)
    {
        
        GameObject[] currentScenePlayer = GameObject.FindGameObjectsWithTag("Player");
        if (currentScenePlayer.Length > 0 && !restorePosition && currentScenePlayer != null)
        {
            _lastPosition = currentScenePlayer[0].transform.position;
        }
        movePlayerOnLoad = restorePosition;
        SceneManager.LoadScene(scene);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen){
            Instantiate(pauseMenu);
        }
    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        choices.RemakeDeps();
        GameObject[] newScenePlayer = GameObject.FindGameObjectsWithTag("Player");
        if (newScenePlayer.Length > 0 && newScenePlayer[0] != null && movePlayerOnLoad)
        {
            CharacterController characterController = newScenePlayer[0].gameObject.GetComponent<CharacterController>();
            characterController.enabled = false;
            newScenePlayer[0].transform.position = _lastPosition;
            characterController.enabled = true;
        }
    }
    
    public void InitializeChoices()
    {
        choices = new Choices();
        choices.AddFlag("Wood", 0);
    }
}
