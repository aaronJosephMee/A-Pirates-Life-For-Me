using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems
{
    ItemStats totalStats = new ItemStats();
    Dictionary<string, Item> playerRelics;
    Dictionary<string, Item> playerWeapons;
    Item currentItem;
    Item currentWeapon;
    public PlayerItems(){
        playerRelics = new Dictionary<string, Item>();
    }
    public int RelicCount(){
        return playerRelics.Count;
    }
    public Item GetItem(){
        return currentItem;
    }
    public void SetItem(Item item){
        currentItem = item;
    }
    public Item GetWeapon(){
        return currentWeapon;
    }
    public void SetWeapon(string weapon){
        if (currentWeapon.type != ""){
            totalStats = SubtractStats(totalStats, GetItemStats(currentWeapon));
        }
        currentWeapon = playerWeapons[weapon];
        totalStats = CombineStats(totalStats, GetItemStats(currentWeapon));
    }
    public Dictionary<string, Item> GetRelics(){
        return playerRelics;
    }
    public void AddRelic(KeyValuePair<string, Item> relic){
        playerRelics.Add(relic.Key, relic.Value);
        totalStats = CombineStats(totalStats, GetItemStats(relic.Value));
    }
    public Dictionary<string, Item> GetWeapons(){
        return playerWeapons;
    }
    public void AddWeapon(KeyValuePair<string, Item> weapon){
        playerWeapons.Add(weapon.Key, weapon.Value);
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
