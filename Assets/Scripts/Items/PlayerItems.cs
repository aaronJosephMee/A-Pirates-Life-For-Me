using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerItems
{
    ItemStats totalStats = new ItemStats();
    Dictionary<string, RelicScriptableObject> playerRelics = new Dictionary<string, RelicScriptableObject>();
    Dictionary<string, WeaponScriptableObject> playerWeapons = new Dictionary<string, WeaponScriptableObject>();
    Dictionary<string, Item> upgradeables = new Dictionary<string, Item>();
    List<string> onMelee = new List<string>();
    List<string> onKill = new List<string>();
    List<string> onTakeDamage = new List<string>();
    Dictionary<string, int> meleeDebuffs = new Dictionary<string, int>();
    Dictionary<string, int> gunDebuffs = new Dictionary<string, int>();

    ItemScriptableObject currentItem;
    public WeaponScriptableObject currentGun;
    public WeaponScriptableObject currentSword;
    int gold = 0;
    public Dictionary<string, int> GetSwordDebuffs(){
        return meleeDebuffs;
    }
    public Dictionary<string, int>GetGunDebuffs(){
        return gunDebuffs;
    }
    public List<string> GetMeleeRelics(){
        return onMelee;
    }
    public List<string> GetKillRelics(){
        return onKill;
    }
    public List<string> GetTakeDamageRelics(){
        return onTakeDamage;
    }
    public Item GetRandUpgrade(){
        if (upgradeables.Count == 0){
            return null;
        }
        System.Random r = new System.Random();
        List<string> keys = new List<string>(upgradeables.Keys);
        return upgradeables[keys[r.Next(keys.Count)]];
    }
    public bool IsUpgrade(Item item){
        return upgradeables.ContainsKey(item.title);
    }
    public void AddGold(int goldToAdd){
        gold += goldToAdd;

        if (gold < 0)
        {
            gold = 0; 
        }
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
    public WeaponScriptableObject GetGun()
    {
        return currentGun;
    }

    public WeaponScriptableObject GetSword()
    {
        return currentSword;
    }

    public void SetGun(string weapon){
        if (currentGun != null){
            totalStats = ItemManager.instance.SubtractStats(totalStats, ItemManager.instance.GetItemStats(currentGun));
            if (currentGun.baseStats.gunDebuff != Debuffs.None){
                gunDebuffs[currentGun.baseStats.gunDebuff.ToString()] = gunDebuffs[currentGun.baseStats.gunDebuff.ToString()] - ItemManager.instance.GetItemStats(currentGun).duration;
                if (gunDebuffs[currentGun.baseStats.gunDebuff.ToString()] <= 0){
                    gunDebuffs.Remove(currentGun.baseStats.gunDebuff.ToString());
                }
            }
        }
        currentGun = playerWeapons[weapon];
        totalStats = ItemManager.instance.CombineStats(totalStats, ItemManager.instance.GetItemStats(currentGun));
        if (currentGun.baseStats.gunDebuff != Debuffs.None){
            if (gunDebuffs.TryGetValue(currentGun.baseStats.gunDebuff.ToString(), out int i)){
                gunDebuffs[currentGun.baseStats.gunDebuff.ToString()] = i + ItemManager.instance.GetItemStats(currentGun).duration;
            }
            else{
                gunDebuffs.Add(currentGun.baseStats.gunDebuff.ToString(), ItemManager.instance.GetItemStats(currentGun).duration);             
            }
        }
    }

    public void SetSword(string weapon)
    {
        if (currentSword != null)
        {
            totalStats = ItemManager.instance.SubtractStats(totalStats, ItemManager.instance.GetItemStats(currentSword));
            if (currentSword.baseStats.swordDebuff != Debuffs.None){
                meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()] = meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()] - ItemManager.instance.GetItemStats(currentSword).duration;
                if (meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()] <= 0){
                    meleeDebuffs.Remove(currentSword.baseStats.swordDebuff.ToString());
                }
            }
        }
        currentSword = playerWeapons[weapon];
        totalStats = ItemManager.instance.CombineStats(totalStats, ItemManager.instance.GetItemStats(currentSword));
        if (currentSword.baseStats.swordDebuff != Debuffs.None){
            if (meleeDebuffs.TryGetValue(currentSword.baseStats.swordDebuff.ToString(), out int i)){
                meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()] = i + ItemManager.instance.GetItemStats(currentSword).duration;
            }
            else{
                meleeDebuffs.Add(currentSword.baseStats.swordDebuff.ToString(), ItemManager.instance.GetItemStats(currentSword).duration);             
            }
        }
    }

    public Dictionary<string, RelicScriptableObject> GetRelics(){
        return playerRelics;
    }
    public void AddRelic(RelicScriptableObject relic){
        RelicScriptableObject r;
        playerRelics.TryGetValue(relic.title, out r);
        if (r != null){
            playerRelics[relic.title].curlvl++;
            if (r.activator == Activators.Passive){
                totalStats = ItemManager.instance.CombineStats(totalStats, relic.lvlStats); 
            }
            if (relic.baseStats.swordDebuff != Debuffs.None){
                if (meleeDebuffs.TryGetValue(relic.baseStats.swordDebuff.ToString(), out int i)){
                    meleeDebuffs[relic.baseStats.swordDebuff.ToString()] = i + relic.lvlStats.duration;
                }
            }
            if (relic.baseStats.gunDebuff != Debuffs.None){
                if (gunDebuffs.TryGetValue(relic.baseStats.gunDebuff.ToString(), out int i)){
                    gunDebuffs[relic.baseStats.gunDebuff.ToString()] = i + relic.lvlStats.duration;
                }
            }
            if (playerRelics[relic.title].curlvl >= relic.maxlvl){
                upgradeables.Remove(relic.title);
            }
        }
        else{
            playerRelics.Add(relic.title, relic);
            Debug.Log("Adding " + relic.title);
            Debug.Log("Curlvl " + relic.curlvl + ", Maxlvl: " + relic.maxlvl);

            if (relic.curlvl < relic.maxlvl){
                upgradeables.Add(relic.title, relic);
            }
            if (relic.activator == Activators.Melee){
                onMelee.Add(relic.title);
            }
            else if (relic.activator == Activators.OnKill){
                onKill.Add(relic.title);
            }
            else if (relic.activator == Activators.OnTakeDamage){
                onTakeDamage.Add(relic.title);
            }
            else{
                totalStats = ItemManager.instance.CombineStats(totalStats, ItemManager.instance.GetItemStats(relic)); 
                
            }
            if (relic.baseStats.swordDebuff != Debuffs.None){
                if (meleeDebuffs.TryGetValue(relic.baseStats.swordDebuff.ToString(), out int i)){
                    meleeDebuffs[relic.baseStats.swordDebuff.ToString()] = i + ItemManager.instance.GetItemStats(relic).duration;
                }
                else{
                    meleeDebuffs.Add(relic.baseStats.swordDebuff.ToString(), ItemManager.instance.GetItemStats(relic).duration);             
                }
            }
            if (relic.baseStats.gunDebuff != Debuffs.None){
                if (gunDebuffs.TryGetValue(relic.baseStats.gunDebuff.ToString(), out int i)){
                    gunDebuffs[relic.baseStats.gunDebuff.ToString()] = i + ItemManager.instance.GetItemStats(relic).duration;
                }
                else{
                    gunDebuffs.Add(relic.baseStats.gunDebuff.ToString(), ItemManager.instance.GetItemStats(relic).duration);             
                }
            }
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
    public IEnumerator AddEffect(RelicScriptableObject relic){
        ItemStats stats = ItemManager.instance.GetItemStats(relic);
        Debug.Log("CurStacks = " + relic.curStacks + ", MaxStacks = " + stats.maxStacks);
        if (relic.curStacks < stats.maxStacks){
            totalStats = ItemManager.instance.CombineStats(totalStats, stats); 
            playerRelics.TryGetValue(relic.title, out RelicScriptableObject r);
            r.curStacks++;
            yield return new WaitForSeconds(stats.duration);
            totalStats = ItemManager.instance.SubtractStats(totalStats, stats);
            r.curStacks--;
        }
    }
}
