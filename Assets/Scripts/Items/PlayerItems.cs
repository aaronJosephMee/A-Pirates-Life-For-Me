using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems
{
    ItemStats totalStats = new ItemStats();
    Dictionary<string, RelicScriptableObject> playerRelics = new Dictionary<string, RelicScriptableObject>();
    Dictionary<string, WeaponScriptableObject> playerWeapons = new Dictionary<string, WeaponScriptableObject>();
    Dictionary<string, Item> upgradeables = new Dictionary<string, Item>();
    ItemScriptableObject currentItem;
    WeaponScriptableObject currentWeapon;
    int gold = 0;
    public Item GetRandUpgrade(){
        if (upgradeables.Count == 0){
            return null;
        }
        System.Random r = new System.Random();
        List<string> keys = new List<string>(upgradeables.Keys);
        return upgradeables[keys[r.Next(keys.Count - 1)]];
    }
    public bool IsUpgrade(Item item){
        return upgradeables.ContainsKey(item.title);
    }
    public void AddGold(int goldToAdd){
        gold += goldToAdd;
    }
    public int CurrentGold(){
        return gold;
    }
    public int RelicCount(){
        return playerRelics.Count;
    }
    public ItemScriptableObject GetItem(){
        return currentItem;
    }
    public void SetItem(ItemScriptableObject item){
        currentItem = item;
    }
    public WeaponScriptableObject GetWeapon(){
        return currentWeapon;
    }
    public void SetWeapon(string weapon){
        if (currentWeapon != null){
            totalStats = ItemManager.instance.SubtractStats(totalStats, ItemManager.instance.GetItemStats(currentWeapon));
        }
        currentWeapon = playerWeapons[weapon];
        totalStats = ItemManager.instance.CombineStats(totalStats, ItemManager.instance.GetItemStats(currentWeapon));
    }
    public Dictionary<string, RelicScriptableObject> GetRelics(){
        return playerRelics;
    }
    public void AddRelic(RelicScriptableObject relic){
        RelicScriptableObject r;
        playerRelics.TryGetValue(relic.title, out r);
        if (r != null){
            playerRelics[relic.title].curlvl++;
            totalStats = ItemManager.instance.CombineStats(totalStats, relic.lvlStats); 
            if (playerRelics[relic.title].curlvl >= relic.maxlvl){
                upgradeables.Remove(relic.title);
            }
        }
        else{
            playerRelics.Add(relic.title, relic);
            if (relic.curlvl < relic.maxlvl){
                upgradeables.Add(relic.title, relic);
            }
            totalStats = ItemManager.instance.CombineStats(totalStats, ItemManager.instance.GetItemStats(relic)); 
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
            // if (playerRelics[weapon.title].curlvl >= weapon.maxlvl){
            //     upgradeables.Remove(weapon.title);
            // }
        }
        // if (weapon.curlvl < weapon.maxlvl){
        //     upgradeables.Add(weapon.title, weapon);
        // }
        playerWeapons.Add(weapon.title, weapon);
    }
    public ItemStats TotalStats(){
        return totalStats;
    }
}
