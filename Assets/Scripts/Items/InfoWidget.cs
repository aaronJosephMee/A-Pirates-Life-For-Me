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
        //this.transform.position = new Vector3(82f,0,0);
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
