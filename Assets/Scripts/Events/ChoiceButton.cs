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
    }
    public void DisplayChoice(Choice choice)
    {
        toDisplay = choice;
        myText.text = choice.text;
    }
    void ChoicePicked()
    {
        foreach (string newEvent in toDisplay.eventsToAdd){
            GameManager.events.AddEvent(newEvent);
        }
        Destroy(this.transform.parent.gameObject);
    }
}
