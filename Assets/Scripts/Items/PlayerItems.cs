using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems
{
    Dictionary<string, Item> playerRelics;
    Dictionary<string, Item> playerWeapons;
    Item currentItem;
    Item currentWeapon;
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
        currentWeapon = playerWeapons[weapon];
    }
    public Dictionary<string, Item> GetRelics(){
        return playerRelics;
    }
    public void AddRelic(KeyValuePair<string, Item> relic){
        playerRelics.Add(relic.Key, relic.Value);
    }
    public Dictionary<string, Item> GetWeapons(){
        return playerWeapons;
    }
    public void AddWeapon(KeyValuePair<string, Item> weapon){
        playerWeapons.Add(weapon.Key, weapon.Value);
    }

}
