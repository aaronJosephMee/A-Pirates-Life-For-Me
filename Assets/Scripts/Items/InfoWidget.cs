using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoWidget : MonoBehaviour
{
    TextMeshProUGUI levelText;
    TextMeshProUGUI infotext;
    private void Start() {
        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tmp in tmps){
            Debug.Log(tmp.name);
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
        string info = "";
        ItemStats stats = item.baseStats;
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
        infotext.text = info;
        levelText.text = "Level: " + item.curlvl;
    }
}
