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
    [SerializeField] private GameObject popUpPrefab;
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
            if (toDisplay.relicsToAdd.Value != null)
            {
                foreach (RelicScriptableObject relic in toDisplay.relicsToAdd.Value)
                {
                    ItemManager.instance.AddRelic(relic);
                }
            }
            if (toDisplay.relicsToLose.Value != null)
            {
                foreach (RelicScriptableObject relic in toDisplay.relicsToLose.Value)
                {
                    ItemManager.instance.LoseRelic(relic);
                }
            }

            // Add gold
            ItemManager.instance.AddGold(toDisplay.stats.gold);
            Health health = ItemManager.instance.GetHealth();
            if (toDisplay.stats.health > 0){
                ItemManager.instance.SetHealth(health.curHealth + toDisplay.stats.health, health.maxHealth + toDisplay.stats.health);
            }
            else if (toDisplay.stats.health < 0){
                health.maxHealth += toDisplay.stats.health;
                if (health.curHealth > health.maxHealth){
                    health.curHealth = health.maxHealth;
                }
                ItemManager.instance.SetHealth(health.curHealth, health.maxHealth);
            }
            
            // TODO: Add logic to reward player with stats and relics
            if (toDisplay.nextScene == SceneName.OverworldMap)
            {
                GameManager.instance.DisplayPopUp(popUpPrefab, SceneName.OverworldMap, toDisplay.followUpText);
            }
            else
            {
                GameManager.instance.StorePopUp(popUpPrefab, SceneName.OverworldMap, toDisplay.followUpText);
                GameManager.instance.LoadScene(toDisplay.nextScene);
            }
            Destroy(this.transform.parent.gameObject);
        }
        else
        {
            if (toDisplay.changeVisuals)
            {
                GameManager.instance.ChangeVisuals(toDisplay.changeIndex);
            }
            gameObject.transform.GetComponentInParent<EventMenu>().UpdateEventMenu(toDisplay.followUpText, toDisplay.nextChoiceIndices.Value);
        }
    }
}
