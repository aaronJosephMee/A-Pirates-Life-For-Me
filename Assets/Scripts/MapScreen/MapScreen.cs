using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{
    public Button StartIsland;
    public Button RustyIsland;
    // Start is called before the first frame update
    void Start()
    {
        StartIsland.onClick.AddListener(delegate{GoIsland("ChoiceTest");});
        RustyIsland.onClick.AddListener(delegate{GoIsland("ChoiceTest2");});
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            GameManager.instance.isMapOpen = false;
            Destroy(this.gameObject);
        }
    }
    void GoIsland(string scene){
        GameManager.instance.isMapOpen = false;
        SceneManager.LoadScene(scene);
    }
}
