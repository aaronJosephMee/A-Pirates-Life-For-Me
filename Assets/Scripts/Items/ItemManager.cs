using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public PlayerItems playerItems = new PlayerItems();
    public ItemPool itemPool;
    public RelicScriptableObject[] relics;
    public ItemScriptableObject[] items;
    public WeaponScriptableObject[] weapons;
    public WeaponScriptableObject defaultWeapon;
    public ItemScriptableObject defaultItem;
    void Awake(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            itemPool = new ItemPool(relics, items, weapons);
            playerItems.AddWeapon(defaultWeapon);
            playerItems.SetWeapon(defaultWeapon.title);
            playerItems.SetItem(defaultItem);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void AddRelic(RelicScriptableObject relic){
        playerItems.AddRelic(relic);
        itemPool.RemoveItem("Relic", relic.title);
    }
    public void AddWeapon(WeaponScriptableObject weapon){
        playerItems.AddWeapon(weapon);
        itemPool.RemoveItem("Weapon", weapon.title);
    }
    public void SetItem(ItemScriptableObject item){
        playerItems.SetItem(item);
    }
    public int RelicCount(){
        return playerItems.RelicCount();
    }
    public ItemScriptableObject GetItem(){
        return playerItems.GetItem();
    }
    public WeaponScriptableObject GetWeapon(){
        return playerItems.GetWeapon();
    }
    public void SetWeapon(string weaponName){
        playerItems.SetWeapon(weaponName);
    }
    public Dictionary<string, RelicScriptableObject> GetRelics(){
        return playerItems.GetRelics();
    }
    public Dictionary<string, WeaponScriptableObject> GetWeapons(){
        return playerItems.GetWeapons();
    }
    public ItemStats TotalStats(){
        return playerItems.TotalStats();
    }
    public ItemScriptableObject GetItem(string title){
        return (ItemScriptableObject)itemPool.GetItem("Item" ,title).Value;
    }
    public RelicScriptableObject GetRelic(string title){
        return (RelicScriptableObject)itemPool.GetItem("Relic" ,title).Value;
    }
    public WeaponScriptableObject GetWeapon(string title){
        return (WeaponScriptableObject)itemPool.GetItem("Weapon" ,title).Value;
    }
    public ItemScriptableObject GetRandItem(string type){
        return (ItemScriptableObject)itemPool.GetRandItem("Item");
    }
    public RelicScriptableObject GetRandRelic(string type){
        return (RelicScriptableObject)itemPool.GetRandItem("Relic");
    }
    public WeaponScriptableObject GetRandWeapon(string type){
        return (WeaponScriptableObject)itemPool.GetRandItem("Weapon");
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
