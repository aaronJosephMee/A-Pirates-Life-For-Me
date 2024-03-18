using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems
{
    ItemStats totalStats = new ItemStats();
    Dictionary<string, RelicScriptableObject> playerRelics = new Dictionary<string, RelicScriptableObject>();
    Dictionary<string, WeaponScriptableObject> playerWeapons = new Dictionary<string, WeaponScriptableObject>();
    ItemScriptableObject currentItem;
    WeaponScriptableObject currentWeapon;
    public int RelicCount(){
        return playerRelics.Count;
    }
    public Item GetItem(){
        return currentItem;
    }
    public void SetItem(ItemScriptableObject item){
        currentItem = item;
    }
    public Item GetWeapon(){
        return currentWeapon;
    }
    public void SetWeapon(string weapon){
        if (currentWeapon != null){
            totalStats = SubtractStats(totalStats, GetItemStats(currentWeapon));
        }
        currentWeapon = playerWeapons[weapon];
        totalStats = CombineStats(totalStats, GetItemStats(currentWeapon));
    }
    public Dictionary<string, RelicScriptableObject> GetRelics(){
        return playerRelics;
    }
    public void AddRelic(RelicScriptableObject relic){
        RelicScriptableObject r;
        playerRelics.TryGetValue(relic.title, out r);
        if (r != null){
            playerRelics[relic.title].curlvl++;
        }
        else{
            playerRelics.Add(relic.title, relic);
            totalStats = CombineStats(totalStats, GetItemStats(relic)); 
        } 
    }
    public Dictionary<string, WeaponScriptableObject> GetWeapons(){
        return playerWeapons;
    }
    public void AddWeapon(WeaponScriptableObject weapon){
        WeaponScriptableObject w;
        playerWeapons.TryGetValue(weapon.title, out w);
        if (w != null){
            playerWeapons[weapon.title].curlvl++;
        }
        playerWeapons.Add(weapon.title, weapon);
    }
    public ItemStats TotalStats(){
        return totalStats;
    }
    public ItemStats CombineStats(ItemStats IS1, ItemStats IS2){
        IS1.duration += IS2.duration;
        IS1.defense += IS2.defense;
        IS1.damage += IS2.damage;
        return IS1;
    }
    public ItemStats SubtractStats(ItemStats IS1, ItemStats IS2){
        IS1.duration -= IS2.duration;
        IS1.defense -= IS2.defense;
        IS1.damage -= IS2.damage;
        return IS1;
    }
    public ItemStats GetItemStats(Item item){
        ItemStats stats = item.baseStats;
        for (int i = 1; i<item.curlvl;i++){
            stats = CombineStats(stats,item.lvlStats);
        }
        return stats;
    }
}
