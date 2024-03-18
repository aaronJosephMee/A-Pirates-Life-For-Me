using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoWidget : MonoBehaviour
{
    TextMeshProUGUI levelText;
    TextMeshProUGUI infotext;
    string info;
    ItemStats stats;
    Item item;
    private void Awake() {
        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tmp in tmps){
            switch(tmp.name){
                case "Level":
                    levelText = tmp;
                    break;
                case "Info":
                    infotext = tmp;
                    break;
            }
        }
    }
    public void GiveItem(Item item){
        info = "";
        stats = item.baseStats;
        this.item = item;
        for (int i = 1; i<item.curlvl;i++){
            stats = ItemManager.instance.playerItems.CombineStats(stats,item.lvlStats);
        }
        if (stats.damage != 0){
            info += "Damage: " + stats.damage + "\n";
        }
        if (stats.defense != 0){
            info += "Defense: " + stats.defense + "\n";
        }
        try{
            if (((ItemScriptableObject) item).uses != 0){
                info += "Uses: " + ((ItemScriptableObject) item).uses + "\n";
            }
        }
        catch{}
        try{
            info += ((WeaponScriptableObject) item).type;
        }
        catch{}
        try{
            if (((RelicScriptableObject) item).activator != Activators.Passive){
                info += "Activation: " + ((RelicScriptableObject) item).activator + "\n";
            }
        }
        catch{}
        if (infotext != null){
            infotext.text = info;
        }
        if (levelText != null){
            levelText.text = "Level: " + item.curlvl;
        }
    }
}
