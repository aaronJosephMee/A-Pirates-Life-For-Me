using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject mapScreen;
    public bool isMapOpen = false;
    public bool ready = false;
    public static Choices choices = new Choices();
    // Start is called before the first frame update
    void Start()
    {
        choices.AddFlag("Blue", 0);
        choices.AddFlag("Orange", 0);
        choices.AddFlag("Yellow", 0);
        choices.AddFlag("Green", 0);
        ready = true;
    }
    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
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
    public void OpenMap(){
        if (!isMapOpen){
            Instantiate(mapScreen, new Vector3(0,0,0), Quaternion.identity);
            isMapOpen = true;
        }
        
    }
}
