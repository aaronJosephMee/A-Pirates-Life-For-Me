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
    private void Start() {
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
        if (infotext != null){
            infotext.text = info;
        }
        if (levelText != null){
            levelText.text = "Level: " + item.curlvl;
        }
    }
    public void GiveItem(Item item){
        info = "";
        Debug.Log(item.baseStats.damage);
        stats = item.baseStats;
        this.item = item;
        for (int i = 0; i<item.curlvl;i++){
            stats = ItemManager.instance.CombineStats(stats,item.lvlStats);
        }
        if (stats.damage != 0){
            info += "Damage: " + stats.damage;
        }
        if (stats.defense != 0){
            info += "Defense: " + stats.defense;
        }
        // if (stats.damage != 0){
        //     info += "Damage: " + stats.damage;
        // }
        if (infotext != null){
            infotext.text = info;
        }
        if (levelText != null){
            levelText.text = "Level: " + item.curlvl;
        }
    }
}
