using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    Choice toDisplay;
    [SerializeField] TextMeshProUGUI myText;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ChoicePicked);
        this.GetComponent<Button>().onClick.AddListener(() => OverworldMapManager.instance.TransitionBackToMap());
    }
    public void DisplayChoice(Choice choice)
    {
        toDisplay = choice;
        myText.text = choice.text;
    }
    void ChoicePicked()
    {
        print("Clicked");
        foreach (string newEvent in toDisplay.eventsToAdd){
            Debug.Log(newEvent);
            OverworldMapManager.instance.AddToEventPool(newEvent);
        }
        Destroy(this.transform.parent.gameObject);
    }
}
