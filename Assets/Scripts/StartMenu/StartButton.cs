using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public SceneName scene;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.menuOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.GetComponent<Button>().onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartGame(){
        GameManager.instance.menuOpen = false;
        Destroy(ItemManager.instance.gameObject);
        GameManager.instance.LoadScene(scene);
    }
}
