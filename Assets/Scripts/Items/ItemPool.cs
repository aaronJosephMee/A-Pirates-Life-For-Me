using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
public enum Debuffs {
    [Description("None")]
    None,
    [Description("Fire")]
    Fire,
    Poison,
    Freeze,
}

[System.Serializable]
public struct ItemStats{
    public int gunDamage;
    public int swordDamage;
    public int defense;
    public int duration;
    public int maxStacks;
    public int bulletCount;
    public float fireRate;
    public float speedBoost;
    public float hpRegen;
    public float projectileSize;
    public Debuffs swordDebuff;
    public Debuffs gunDebuff;
    public float accuracy;
    public float critChance;
    public float critMultiplier;
    public int richochet;
    public float dodgeChance;
    public float healthBoost;
}
public class ItemPool
{
    System.Random random = new System.Random();
    Dictionary<string, WeaponScriptableObject> weaponPool = new Dictionary<string, WeaponScriptableObject>();
    Dictionary<string, ItemScriptableObject> itemPool = new Dictionary<string, ItemScriptableObject>();
    Dictionary<string, RelicScriptableObject> relicPool = new Dictionary<string, RelicScriptableObject>();

    public ItemPool(RelicScriptableObject[] relics, ItemScriptableObject[] items, WeaponScriptableObject[] weapons){
        foreach (RelicScriptableObject relic in relics){
            relicPool.Add(relic.title, relic);
        }
        foreach (ItemScriptableObject item in items){
            itemPool.Add(item.title, item);
        }
        foreach (WeaponScriptableObject weapon in weapons){
            weaponPool.Add(weapon.title, weapon);
        }
    }
    public KeyValuePair<string, Item> GetItem(string type, string title){
        if (type == "Weapon"){
            return new KeyValuePair<string, Item>(title, weaponPool[title]);
        }
        if (type == "Relic"){
            return new KeyValuePair<string, Item>(title, relicPool[title]);
        }
        return new KeyValuePair<string, Item>(title, itemPool[title]);
    }
    public Item GetRandItem(string type){
        List<string> keys;
        int r;
        if (type == "Weapon"){
            keys = new List<string>(weaponPool.Keys);
            if (keys.Count == 0){
                return null;
            }
            r = random.Next(keys.Count);
            return weaponPool[keys[r]];
        }
        if (type == "Relic"){
            keys = new List<string>(relicPool.Keys);
            if (keys.Count == 0){
                return null;
            }
            r = random.Next(keys.Count);
            return relicPool[keys[r]];
        }
        keys = new List<string>(itemPool.Keys);
        if (keys.Count == 0){
            return null;
        }
        r = random.Next(keys.Count);
        return itemPool[keys[r]];
    }

    public void RemoveItem(string type, string title){
        if (type == "Weapon"){
            weaponPool.Remove(title);
        }
        if (type == "Relic"){
            relicPool.Remove(title);
        }
        itemPool.Remove(title);
    }

    // string[] allStats = new string[] {"Damage", "Defense", "Duration"};
    // Dictionary<String, Item> LoadItems(string filename){
    //     Dictionary<String, Item> res = new Dictionary<string, Item>();

    //     int i = 0;
    //     String[] items = File.ReadAllLines(Application.streamingAssetsPath + "/Items/" + filename);
    //     while (i < items.Length){
    //         Item itm = new Item();
    //         string internalName = items[i];
    //         i++;
    //         itm.name = items[i];
    //         i++;
    //         itm.type = items[i];
    //         i++;
    //         itm.imageName = items[i];
    //         i++;
    //         ItemStats baseStats = new ItemStats();
    //         foreach (string stat in allStats){
    //             switch(items[i].Split(" ")[0]){
    //                 case "Damage":
                        
    //                     baseStats.damage = int.Parse(items[i].Split(" ")[1]);
    //                     i++;
    //                     break;
    //                 case "Defense":
    //                     baseStats.defense = int.Parse(items[i].Split(" ")[1]);                            
    //                     i++;
    //                     break;
    //                 case "Duration":
    //                     baseStats.duration = int.Parse(items[i].Split(" ")[1]);
    //                     i++;
    //                     break;
    //                 default:
    //                     break;
    //             }
    //         }
    //         itm.baseStats = baseStats;
    //         itm.maxlvl = int.Parse(items[i]);
    //         i++;
    //         itm.curlvl = int.Parse(items[i]);
    //         i++;
    //         ItemStats lvlStats = new ItemStats();
    //         foreach (string stat in allStats){
    //             switch(items[i].Split(" ")[0]){
    //                 case "Damage":
    //                     lvlStats.damage = int.Parse(items[i].Split(" ")[1]);
    //                     i++;
    //                     break;
    //                 case "Defense":
    //                     lvlStats.defense = int.Parse(items[i].Split(" ")[1]);                            
    //                     i++;
    //                     break;
    //                 case "Duration":
    //                     lvlStats.duration = int.Parse(items[i].Split(" ")[1]);
    //                     i++;
    //                     break;
    //                 default:
    //                     break;
    //             }
    //         }
    //         itm.lvlStats = lvlStats;
    //         if (itm.type == "Relic"){
    //             itm.activators = items[i];
    //             i++;
    //         }
    //         else if (itm.type == "Item"){
    //             itm.uses = int.Parse(items[i]);
    //             i++;
    //         }
    //         res.Add(internalName, itm);
    //     }

    //     return res;
    // }
}
