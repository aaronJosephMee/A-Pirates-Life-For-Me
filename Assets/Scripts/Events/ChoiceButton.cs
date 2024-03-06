using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    Choice toDisplay;
    [SerializeField] TextMeshProUGUI myText;
    // Start is called before the first frame update
    void Start()
    {
        // myText = GetComponentInChildren<TextMeshProUGUI>();
        // myText = GetComponent<TextMeshProUGUI>();
        this.GetComponent<Button>().onClick.AddListener(ChoicePicked);
        
    }
    public void DisplayChoice(Choice choice){
        toDisplay = choice;
        myText.text = choice.text;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void ChoicePicked(){
        foreach (string newEvent in toDisplay.eventsToAdd){
            GameManager.events.AddEvent(newEvent);
        }
        Destroy(this.transform.parent.gameObject);
    }
}
