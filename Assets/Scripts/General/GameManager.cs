using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject mapScreen;
    public static Choices choices = new Choices();
    // Start is called before the first frame update
    void Start()
    {
        choices.AddFlag("Blue", 0);
        choices.AddFlag("Orange", 0);
        choices.AddFlag("Yellow", 0);
        choices.AddFlag("Green", 0);
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

}
