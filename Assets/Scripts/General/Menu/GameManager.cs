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
    private bool movePlayerOnLoad = false;
    private int _cameraIndex = 0;
    
    public bool menuOpen;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    private Canvas canvas;
    
    public SceneName currentScene = SceneName.TitleScreen;

    private GameObject storedPopUp;
    private SceneName storedPopUpNextScene;
    private String storedPopUpFollowUpText;
    
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

    private void Start()
    {
        Canvas currentCanvas = GetComponentInParent<Canvas>();
        canvas = currentCanvas;
    }

    public void LoadScene(SceneName scene, int cameraIndex=0)
    {
        _cameraIndex = cameraIndex;
        if (currentScene.Equals(SceneName.OverworldMap) && scene.Equals(SceneName.TitleScreen))
        {
            OverworldMapManager.Instance?.MarkMapForReset();
        }
        GameObject[] currentScenePlayer = GameObject.FindGameObjectsWithTag("Player");
        currentScene = scene;
        SceneManager.LoadScene(scene.GetSceneString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen){
            Instantiate(pauseMenu);
        }

        if (menuOpen && canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        GameObject cameras = GameObject.Find("NarrativeCameras");
        if (cameras != null)
        {
            SelectEventCamera(cameras);
            ToggleEventToggleable(cameras);
        } 
        
        GameObject[] newScenePlayer = GameObject.FindGameObjectsWithTag("Player");
        if (newScenePlayer.Length > 0 && newScenePlayer[0] != null && movePlayerOnLoad)
        {
            CharacterController characterController = newScenePlayer[0].gameObject.GetComponent<CharacterController>();
            characterController.enabled = false;
            newScenePlayer[0].transform.position = _lastPosition;
            characterController.enabled = true;
        }
    }

    private void SelectEventCamera(GameObject cameras)
    {
        if (_cameraIndex != 0)
        {
            foreach (Transform cam in cameras.transform)
            {
                if (!cam.gameObject.name.Contains( _cameraIndex.ToString()))
                {
                    Destroy(cam.gameObject);
                }
            }
        }
    }

    private void ToggleEventToggleable(GameObject cameras)
    {
        cameras.GetComponent<IEventToggleable>()?.ToggleEventEffects(_cameraIndex);
    }

    public void DisplayPopUp(GameObject popUpPrefab, SceneName nextScene, String followUpText)
    {
        GameObject popUpGameObject = Instantiate(popUpPrefab);
        PopUpScreen popUpScreen = popUpGameObject.GetComponent<PopUpScreen>();
        popUpScreen.AssignContinueLocation(nextScene);
        popUpScreen.SetText(followUpText);
    }

    public void StorePopUp(GameObject popUpPrefab, SceneName nextScene, String followUpText)
    {
        storedPopUp = popUpPrefab;
        storedPopUpNextScene = nextScene;
        storedPopUpFollowUpText = followUpText;
    }

    public void DisplayStoredPopUp()
    {
        if (storedPopUp != null)
        {
            DisplayPopUp(storedPopUp, storedPopUpNextScene, storedPopUpFollowUpText);
            storedPopUp = null;
        }
        else
        {
            LoadScene(SceneName.OverworldMap);
        }
    }

    public bool HasPopup()
    {
        return storedPopUp != null;
    }
}
