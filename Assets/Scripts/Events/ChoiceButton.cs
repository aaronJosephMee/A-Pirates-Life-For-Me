using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
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
        if (toDisplay.isTerminal)
        {
            // Add events
            foreach (EventScriptableObject newEvent in toDisplay.eventsToAdd.Value){
                OverworldMapManager.Instance.AddToEventPool(newEvent);
            }
            
            // Add relics
            foreach (RelicScriptableObject relic in toDisplay.relics.Value)
            {
                ItemManager.instance.AddRelic(relic);
            }
            
            // Add gold
            ItemManager.instance.AddGold(toDisplay.stats.gold);
            // TODO: Add logic to reward player with stats and relics
            Destroy(this.transform.parent.gameObject);
            GameManager.instance.LoadScene(toDisplay.nextScene);
        }
        else
        {
            gameObject.transform.GetComponentInParent<EventMenu>().UpdateEventMenu(toDisplay.followUpText, toDisplay.nextChoiceIndices.Value);
        }
    }
}
