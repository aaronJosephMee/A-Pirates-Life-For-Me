using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;
using UnityEngine.UI;

public class ReturnTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ReturnToTitle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ReturnToTitle(){
        GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>()?.DisablePlayerInput();
        GameManager.instance.LoadScene(SceneName.TitleScreen);
    }
}
