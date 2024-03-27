using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InfoWidget : MonoBehaviour
{
    TextMeshProUGUI levelText;
    TextMeshProUGUI infotext;
    string info;
    public bool ownedItem = false;
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
        if (stats.hpRegen != 0){
            info += "Health Regen: " + stats.hpRegen + "hp/sec\n";
        }
        if (stats.speedBoost != 0){
            info += "Speed: " + stats.speedBoost + "\n";
        }
        if (stats.critChance != 0){
            info += "Crit Chance: " + stats.critChance + "\n";
        }
        if (stats.critMultiplier != 0){
            info += "Crit Multiplier: " + stats.critMultiplier + "\n";
        }
        if (stats.richochet != 0){
            info += "Ricochets: " + stats.richochet + "\n";
        }
        if (stats.gunDebuff != Debuffs.None){
            info += "Gun Debuff: " + stats.gunDebuff + "\n";
        }
        if (stats.swordDebuff != Debuffs.None){
            info += "Sword Debuff: " + stats.swordDebuff + "\n";
        }
        if (stats.duration != 0){
            info += "Duration: " + stats.duration + "\n";
        }
        if (stats.maxStacks != 0 && stats.gunDebuff != Debuffs.Fire){
            info += "Max Stacks: " + stats.maxStacks + "\n";
        }
        if (info != ""){
            info = "Passive Effects: \n" + info;
        }
        try{
            if (((ItemScriptableObject) item).uses != 0){
                if (ownedItem){
                    info += "Uses Left: " + (((ItemScriptableObject) item).uses - ItemManager.instance.GetItemUses()) + "\n";
                }
                else{
                    info += "Uses: " + ((ItemScriptableObject) item).uses + "\n"; 
                }
                info += "Cooldown: " + ((ItemScriptableObject)item).cooldown + " sec\n";
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
                if (((RelicScriptableObject) item).activator == Activators.OnTakeDamage){
                    info += "Condition: Take Damage\n";
                }
                else if (((RelicScriptableObject) item).activator == Activators.OnKill){
                    info += "Condition: On Kill\n";
                }
                else{
                    info += "Condition: " + ((RelicScriptableObject) item).activator + "\n";
                }
                
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
                if (stats.hpRegen != 0){
                    info += "Health Regen: " + stats.hpRegen + "hp/sec\n";
                }
                if (stats.speedBoost != 0){
                    info += "Speed: " + stats.speedBoost + "\n";
                }
                if (stats.critChance != 0){
                    info += "Crit Chance: " + stats.critChance + "\n";
                }
                if (stats.critMultiplier != 0){
                    info += "Crit Multiplier: " + stats.critMultiplier + "\n";
                }
                if (stats.richochet != 0){
                    info += "Ricochets: " + stats.richochet + "\n";
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
        if (item.lvlStats.hpRegen != 0){
            info += "Health Regen: " + stats.hpRegen + " -> " + (stats.hpRegen + item.lvlStats.hpRegen) + "\n";
        }
        if (item.lvlStats.speedBoost != 0){
            info += "Speed: " + stats.speedBoost + " -> " + (stats.speedBoost + item.lvlStats.speedBoost) + "\n";
        }
        if (item.lvlStats.critChance != 0){
            info += "Crit Chance: " + stats.critChance + " -> " + (stats.critChance + item.lvlStats.critChance) + "\n";
        }
        if (item.lvlStats.critMultiplier != 0){
            info += "Crit Multiplier: " + stats.critMultiplier + " -> " + (stats.critMultiplier + item.lvlStats.critMultiplier) + "\n";
        }
        if (item.lvlStats.richochet != 0){
            info += "Ricochets: " + stats.richochet + " -> " + (stats.richochet + item.lvlStats.richochet) + "\n";
        }
        if (item.lvlStats.duration != 0){
            info += "Duration: " + stats.duration + " -> " + (stats.duration + item.lvlStats.duration) + "\n";
        }
        if (item.lvlStats.maxStacks != 0){
            info += "Max Stacks: " + stats.maxStacks + " -> " + (stats.maxStacks + item.lvlStats.maxStacks) + "\n";
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
                if (relic.Activatorlvl.hpRegen != 0){
                    active += "Health Regen: " + stats.hpRegen + " -> " + (stats.hpRegen + relic.Activatorlvl.hpRegen) + "\n";
                }
                if (relic.Activatorlvl.speedBoost != 0){
                    active += "Speed: " + stats.speedBoost + " -> " + (stats.speedBoost + relic.Activatorlvl.speedBoost) + "\n";
                }
                if (relic.Activatorlvl.critChance != 0){
                    active += "Crit Chance: " + stats.critChance + " -> " + (stats.critChance + relic.Activatorlvl.critChance) + "\n";
                }
                if (relic.Activatorlvl.critMultiplier != 0){
                    active += "Crit Multiplier: " + stats.critMultiplier + " -> " + (stats.critMultiplier + relic.Activatorlvl.critMultiplier) + "\n";
                }
                if (relic.Activatorlvl.richochet != 0){
                    active += "Ricochets: " + stats.richochet + " -> " + (stats.richochet + relic.Activatorlvl.richochet) + "\n";
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
