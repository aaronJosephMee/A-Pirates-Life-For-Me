using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        if (info != ""){
            info = "Passive Effects: \n" + info;
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
                info += "Active Effects: \n";
                info += "Condition: " + ((RelicScriptableObject) item).activator + "\n";
                stats = ItemManager.instance.GetActivatorStats((RelicScriptableObject)item);
                info += "Duration: " + stats.duration + " sec\n";
                info += "Max Stacks: " + stats.maxStacks + "\n";
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
        if (info != ""){
            info = "Passive Effects: \n" + info;
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
                stats = ItemManager.instance.GetActivatorStats((RelicScriptableObject)item);
                RelicScriptableObject relic = (RelicScriptableObject) item;
                string active = "";
                if (relic.Activatorlvl.duration != 0){
                    active += "Duration: " + stats.duration + " -> " + (stats.duration + relic.Activatorlvl.duration) + " sec\n";
                }
                if (relic.Activatorlvl.maxStacks != 0){
                    active += "Max Stacks: " + stats.maxStacks + " -> " + (stats.maxStacks + relic.Activatorlvl.maxStacks) + "\n";
                }
                if (relic.Activatorlvl.gunDamage != 0){
                    active += "Gun Damage: " + stats.gunDamage + " -> " + (stats.gunDamage + relic.Activatorlvl.gunDamage) + "\n";
                }
                if (relic.Activatorlvl.defense != 0){
                    active += "Defense: " + stats.defense + " -> " + (stats.defense + relic.Activatorlvl.defense) + "\n";
                }
                if (relic.Activatorlvl.swordDamage != 0){
                    active += "Sword Damage: " + stats.swordDamage + " -> " + (stats.swordDamage + relic.Activatorlvl.swordDamage) + "\n";
                }
                if (relic.Activatorlvl.bulletCount != 0){
                    active += "Extra Bullets: " + stats.bulletCount + " -> " + (stats.bulletCount + relic.Activatorlvl.bulletCount) + "\n";
                }
                if (relic.Activatorlvl.fireRate != 0){
                    active += "Fire rate: " + stats.fireRate + " -> " + (stats.fireRate + relic.Activatorlvl.fireRate) + "\n";
                }
                if (relic.Activatorlvl.accuracy != 0){
                    active += "Accuracy: " + stats.accuracy + " -> " + (stats.accuracy + relic.Activatorlvl.accuracy) + "\n";
                }
                if (relic.Activatorlvl.projectileSize != 0){
                    active += "Bullet Size: " + stats.projectileSize + " -> " + (stats.projectileSize + relic.Activatorlvl.projectileSize) + "\n";
                }
                if (active != ""){
                    active = "Active Effects: \n" + active;
                }
                info += active;
                
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
