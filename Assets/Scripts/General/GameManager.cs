using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Choices choices = new Choices();
    private static Vector3 _lastPosition;
    // Start is called before the first frame update
    void Start()
    {
    }
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

    public void LoadScene(string scene, bool restorePosition)
    {
        GameObject currentScenePlayer = GameObject.FindGameObjectsWithTag("Player")[0].gameObject;
        Vector3 newLastPosition = currentScenePlayer.transform.position;
        SceneManager.LoadScene(scene);
        if (restorePosition)
        {
            GameObject newScenePlayer = GameObject.FindGameObjectsWithTag("Player")[0].gameObject;
            if (newScenePlayer != null)
            {
                newScenePlayer.transform.position = _lastPosition;
            }
        }
        _lastPosition = newLastPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        choices.RemakeDeps();
    }

}
