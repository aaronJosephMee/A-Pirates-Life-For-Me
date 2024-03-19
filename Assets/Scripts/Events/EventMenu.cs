using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventMenu : MonoBehaviour
{
    EventScriptableObject toDisplay;
    ButtonSpawner buttonSpawner;
    TextMeshProUGUI title;
    TextMeshProUGUI bodyText;
    private List<GameObject> buttons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        buttonSpawner = GetComponentInChildren<ButtonSpawner>();
        toDisplay = OverworldMapManager.Instance.GetEvent();
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
        title.text = toDisplay.title;
        bodyText.text = toDisplay.flavorText;
        buttons = buttonSpawner.Spawn(GetChoicesFromIndices(toDisplay.initialChoices));
    }

    private Choice[] GetChoicesFromIndices(int[] indices)
    {
        Choice[] choices = new Choice[indices.Length];
        for (int i = 0; i < indices.Length; i++)
        {
            choices[i] = toDisplay.choices.Value[indices[i]];
        }

        return choices;
    }

    public void UpdateEventMenu(String followUpText, int[] followUpChoices)
    {
        bodyText.text = followUpText;
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }

        buttonSpawner.Spawn(GetChoicesFromIndices(followUpChoices));
    }
}
