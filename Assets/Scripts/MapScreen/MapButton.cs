using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private Button mapButton;
    public string dest;
    public string onFlag;
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        mapButton = this.GetComponent<Button>();
        mapButton.onClick.AddListener(delegate{GoIsland(dest);});
        mapButton.interactable = false;
        if (!onFlag.Equals("")){
            GameManager.choices.CreateDependency(onFlag, this.gameObject, 1, Activate);
        }
        else if (!dest.Equals("")){
            Activate(this.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GoIsland(string scene){
        GameManager.instance.LoadScene(scene, false);
    }
    int Activate(GameObject gameObject){
        mapButton.interactable = true;
        this.GetComponent<Image>().color = new Color(0,0,0,0);
        isActive = true;
        return 0;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActive){
            this.GetComponent<Image>().color = new Color(255/255,255/255,0,60f/255);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isActive){
            this.GetComponent<Image>().color = new Color(0,0,0,0);
        }
    }
}
