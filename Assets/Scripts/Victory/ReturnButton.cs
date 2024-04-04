using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    public Button title;
    // Start is called before the first frame update
    void Start()
    {
        title.onClick.AddListener(delegate{
            OverworldMapManager.Instance?.MarkMapForReset();
            GameManager.instance.LoadScene(SceneName.TitleScreen);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
