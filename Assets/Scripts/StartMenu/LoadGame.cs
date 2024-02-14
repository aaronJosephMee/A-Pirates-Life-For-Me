using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button load = this.GetComponent<Button>();
        if (File.Exists(Application.dataPath + "/Saves/savefile")){
            load.onClick.AddListener(Load);
        }
        else{
            load.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Load(){
        GameManager.instance.menuOpen = false;
        GameManager.choices.LoadState();
    }
}
