using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DefaultNamespace.OverworldMap;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Choices choices;
    private static Vector3 _lastPosition;
    public bool menuOpen;
    private bool movePlayerOnLoad = false;
    public GameObject pauseMenu;
    public SceneName currentScene = SceneName.TitleScreen;
    // Start is called before the first frame update
    void Awake(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(SceneName scene, bool restorePosition)
    {
        if (currentScene.Equals(SceneName.OverworldMap) && scene.Equals(SceneName.TitleScreen))
        {
            OverworldMapManager.Instance?.MarkMapForReset();
        }
        GameObject[] currentScenePlayer = GameObject.FindGameObjectsWithTag("Player");
        if (currentScenePlayer.Length > 0 && !restorePosition && currentScenePlayer != null)
        {
            _lastPosition = currentScenePlayer[0].transform.position;
        }
        movePlayerOnLoad = restorePosition;
        currentScene = scene;
        SceneManager.LoadScene(scene.GetSceneString());
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
        GameObject[] newScenePlayer = GameObject.FindGameObjectsWithTag("Player");
        if (newScenePlayer.Length > 0 && newScenePlayer[0] != null && movePlayerOnLoad)
        {
            CharacterController characterController = newScenePlayer[0].gameObject.GetComponent<CharacterController>();
            characterController.enabled = false;
            newScenePlayer[0].transform.position = _lastPosition;
            characterController.enabled = true;
        }
    }
}
