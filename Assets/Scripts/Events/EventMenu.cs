using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventMenu : MonoBehaviour
{
    Event toDisplay;
    ButtonSpawner buttonSpawner;
    TextMeshProUGUI title;
    TextMeshProUGUI bodyText;
    // Start is called before the first frame update
    void Start()
    {
        buttonSpawner = GetComponentInChildren<ButtonSpawner>();
        toDisplay = GameManager.events.GetEvent();
        //Debug.Log(toDisplay.name);
        
        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tmp in tmps){
            Debug.Log(tmp.name);
            switch(tmp.name){
                case "Name":
                    title = tmp;
                    break;
                case "Body Text":
                    bodyText = tmp;
                    break;
            }
        }
        title.text = toDisplay.name;
        bodyText.text = toDisplay.flavorText;
        buttonSpawner.Spawn(toDisplay.choices);
    }
}
