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
        this.item = item;
    }
    public void SetTextTotal(){
        info = "";
        ItemStats stats;
        stats = item.baseStats;
        
        for (int i = 1; i<item.curlvl;i++){
            stats = ItemManager.instance.CombineStats(stats,item.lvlStats);
        }
        if (stats.gunDamage != 0){
            info += "Gun Damage: " + stats.gunDamage + "\n";
        }
        if (stats.swordDamage != 0){
            info += "Sword Damage: " + stats.swordDamage + "\n";
        }
        if (stats.bulletCount != 0){
            info += "Extra Bullets: " + stats.bulletCount + "\n";
        }
        if (stats.fireRate != 0){
            info += "Fire rate: " + stats.fireRate + "\n";
        }
        if (stats.accuracy != 0){
            info += "Accuracy: " + stats.accuracy + "\n";
        }
        if (stats.projectileSize != 0){
            info += "Bullet Size: " + stats.projectileSize + "\n";
        }
        if (stats.gunDebuff != Debuffs.None){
            info += "Gun Debuff: " + stats.gunDebuff + "\n";
        }
        if (stats.swordDebuff != Debuffs.None){
            info += "Sword Debuff: " + stats.swordDebuff + "\n";
        }
        try{
            if (((ItemScriptableObject) item).uses != 0){
                info += "Uses: " + ((ItemScriptableObject) item).uses + "\n";
            }
            info += "Class: Item\n";
        }
        catch{}
        try{
            info += "Type: " + ((WeaponScriptableObject) item).type + "\n";
            info += "Class: Weapon\n";
        }
        catch{}
        try{
            if (((RelicScriptableObject) item).activator != Activators.Passive){
                info += "Activation: " + ((RelicScriptableObject) item).activator + "\n";
                info += "Duration: " + stats.duration + " sec\n";
                info += "Max Stacks: " + stats.maxStacks + "\n";
            }
            info += "Class: Relic\n";
        }
        catch{}
        if (infotext != null){
            infotext.text = info;
        }
        if (levelText != null){
            if (item.curlvl < item.maxlvl){
                levelText.text = "Level: " + item.curlvl;
            }
            else{
                levelText.text = "Level: MAX";
            }
        }
    }
    public void SetTextLevel(){
        info = "";
        ItemStats stats;
        stats = item.baseStats;
        
        for (int i = 1; i<item.curlvl;i++){
            stats = ItemManager.instance.CombineStats(stats,item.lvlStats);
        }
        if (item.lvlStats.gunDamage != 0){
            info += "Gun Damage: " + stats.gunDamage + " -> " + (stats.gunDamage + item.lvlStats.gunDamage) + "\n";
        }
        if (item.lvlStats.defense != 0){
            info += "Defense: " + stats.defense + " -> " + (stats.defense + item.lvlStats.defense) + "\n";
        }
        if (item.lvlStats.swordDamage != 0){
            info += "Sword Damage: " + stats.swordDamage + " -> " + (stats.swordDamage + item.lvlStats.swordDamage) + "\n";
        }
        if (item.lvlStats.bulletCount != 0){
            info += "Extra Bullets: " + stats.bulletCount + " -> " + (stats.bulletCount + item.lvlStats.bulletCount) + "\n";
        }
        if (item.lvlStats.fireRate != 0){
            info += "Fire rate: " + stats.fireRate + " -> " + (stats.fireRate + item.lvlStats.fireRate) + "\n";
        }
        if (item.lvlStats.accuracy != 0){
            info += "Accuracy: " + stats.accuracy + " -> " + (stats.accuracy + item.lvlStats.accuracy) + "\n";
        }
        if (item.lvlStats.projectileSize != 0){
            info += "Bullet Size: " + stats.projectileSize + " -> " + (stats.projectileSize + item.lvlStats.projectileSize) + "\n";
        }
        try{
            if (((ItemScriptableObject) item).uses != 0){
                info += "Uses: " + ((ItemScriptableObject) item).uses + "\n";
            }
            info += "Class: Item\n";
        }
        catch{}
        try{
            info += "Type: " + ((WeaponScriptableObject) item).type + "\n";
            info += "Class: Weapon\n";
        }
        catch{}
        try{
            if (((RelicScriptableObject) item).activator != Activators.Passive){
                if (item.lvlStats.duration != 0){
                    info += "Duration: " + stats.duration + " -> " + (stats.duration + item.lvlStats.duration) + " sec\n";
                }
                if (item.lvlStats.maxStacks != 0){
                    info += "Max Stacks: " + stats.maxStacks + " -> " + (stats.maxStacks + item.lvlStats.maxStacks) + "\n";
                }
            }
            info += "Class: Relic\n";
        }
        catch{}
        if (infotext != null){
            infotext.text = info;
        }
        if (levelText != null){
            if (item.curlvl + 1 < item.maxlvl){
                levelText.text = "Level: " + item.curlvl + " -> " + (item.curlvl + 1);
            }
            else{
                levelText.text = "Level: " + item.curlvl + " -> MAX";
            }
        }
    }
}
