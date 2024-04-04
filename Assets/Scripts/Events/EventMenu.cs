using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventMenu : MonoBehaviour
{
    EventScriptableObject toDisplay;
    ButtonSpawner buttonSpawner;
    [SerializeField] private TextMeshProUGUI bodyText;
    private List<GameObject> buttons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        buttonSpawner = GetComponentInChildren<ButtonSpawner>();
        toDisplay = OverworldMapManager.Instance.GetEvent();
        
        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tmp in tmps){
            switch(tmp.name){
                case "Body Text":
                    bodyText = tmp;
                    break;
            }
        }

        bodyText.text = "";
        StartCoroutine(WriteText(toDisplay.flavorText, toDisplay.initialChoices, false));
    }
    
    IEnumerator WriteText(String textToWrite, int[] choices, bool followUp)
    {
        if (!followUp)
        {
            yield return new WaitForSeconds(0.5f);
        }
        int characterIndex = 0;
        float typingSpeed = 0.02f;
        while (characterIndex < textToWrite.Length) {
            yield return new WaitForSeconds(typingSpeed);
            bodyText.text += textToWrite[characterIndex];
            characterIndex+= 1; 
        }
        buttons = buttonSpawner.Spawn(GetChoicesFromIndices(choices));
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
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
        bodyText.text = "";
        StartCoroutine(WriteText(followUpText, followUpChoices, true));
    }
}
