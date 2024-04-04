using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public struct DebuffStats{
    public int maxStacks;
    public int duration;
}

public class PlayerItems
{
    ItemStats totalStats = new ItemStats();
    Dictionary<string, RelicScriptableObject> playerRelics = new Dictionary<string, RelicScriptableObject>();
    Dictionary<string, WeaponScriptableObject> playerWeapons = new Dictionary<string, WeaponScriptableObject>();
    Dictionary<string, Item> upgradeables = new Dictionary<string, Item>();
    List<string> onMelee = new List<string>();
    List<string> onKill = new List<string>();
    List<string> onTakeDamage = new List<string>();
    List<string> onDodge = new List<string>();
    Dictionary<string, DebuffStats> meleeDebuffs = new Dictionary<string, DebuffStats>();
    Dictionary<string, DebuffStats> gunDebuffs = new Dictionary<string, DebuffStats>();

    ItemScriptableObject currentItem;
    public WeaponScriptableObject currentGun;
    public WeaponScriptableObject currentSword;
    public int itemUses = 0;
    int gold = 100;
    int numbStoryRelics = 0;
    public Dictionary<string, DebuffStats> GetSwordDebuffs(){
        return meleeDebuffs;
    }
    public Dictionary<string, DebuffStats>GetGunDebuffs(){
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
    public List<string> GetOnDodgeRelics(){
        return onDodge;
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
        return playerRelics.Count - numbStoryRelics;
    }
    public ItemScriptableObject GetItem(){
        return currentItem;
    }
    public void SetItem(ItemScriptableObject item){
        currentItem = item;
        itemUses = 0;
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
                DebuffStats stats = new DebuffStats{
                    duration = gunDebuffs[currentGun.baseStats.gunDebuff.ToString()].duration - ItemManager.instance.GetItemStats(currentGun).duration,
                    maxStacks = gunDebuffs[currentGun.baseStats.gunDebuff.ToString()].maxStacks - ItemManager.instance.GetItemStats(currentGun).maxStacks,
                };
                gunDebuffs[currentGun.baseStats.gunDebuff.ToString()] = stats;
                if (gunDebuffs[currentGun.baseStats.gunDebuff.ToString()].duration <= 0){
                    gunDebuffs.Remove(currentGun.baseStats.gunDebuff.ToString());
                }
            }
        }
        currentGun = playerWeapons[weapon];
        totalStats = ItemManager.instance.CombineStats(totalStats, ItemManager.instance.GetItemStats(currentGun));
        if (currentGun.baseStats.gunDebuff != Debuffs.None){
            if (gunDebuffs.TryGetValue(currentGun.baseStats.gunDebuff.ToString(), out DebuffStats i)){
                i.duration = i.duration + ItemManager.instance.GetItemStats(currentGun).duration;
                i.maxStacks = i.maxStacks + ItemManager.instance.GetItemStats(currentGun).maxStacks;
                gunDebuffs[currentGun.baseStats.gunDebuff.ToString()] = i;
            }
            else{
                DebuffStats stats = new DebuffStats{
                    duration = ItemManager.instance.GetItemStats(currentGun).duration,
                    maxStacks = ItemManager.instance.GetItemStats(currentGun).maxStacks,
                };
                gunDebuffs.Add(currentGun.baseStats.gunDebuff.ToString(), stats);             
            }
        }
    }

    public void SetSword(string weapon)
    {
        if (currentSword != null)
        {
            totalStats = ItemManager.instance.SubtractStats(totalStats, ItemManager.instance.GetItemStats(currentSword));
            if (currentSword.baseStats.swordDebuff != Debuffs.None){
                DebuffStats stats = new DebuffStats{
                    duration = meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()].duration - ItemManager.instance.GetItemStats(currentSword).duration,
                    maxStacks = meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()].maxStacks - ItemManager.instance.GetItemStats(currentSword).maxStacks,
                };
                meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()] = stats;
                if (meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()].duration <= 0){
                    meleeDebuffs.Remove(currentSword.baseStats.swordDebuff.ToString());
                }
            }
        }
        currentSword = playerWeapons[weapon];
        totalStats = ItemManager.instance.CombineStats(totalStats, ItemManager.instance.GetItemStats(currentSword));
        if (currentSword.baseStats.swordDebuff != Debuffs.None){
            if (meleeDebuffs.TryGetValue(currentSword.baseStats.swordDebuff.ToString(), out DebuffStats i)){
                i.duration = i.duration + ItemManager.instance.GetItemStats(currentSword).duration;
                i.maxStacks = i.maxStacks + ItemManager.instance.GetItemStats(currentSword).maxStacks;
                meleeDebuffs[currentSword.baseStats.swordDebuff.ToString()] = i;
            }
            else{
                DebuffStats stats = new DebuffStats{
                    duration = ItemManager.instance.GetItemStats(currentSword).duration,
                    maxStacks = ItemManager.instance.GetItemStats(currentSword).maxStacks,
                };
                meleeDebuffs.Add(currentSword.baseStats.swordDebuff.ToString(), stats);             
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
            totalStats = ItemManager.instance.CombineStats(totalStats, relic.lvlStats); 
            Health health = ItemManager.instance.GetHealth();
            if (relic.lvlStats.healthBoost > 0){
                ItemManager.instance.SetHealth(health.curHealth + relic.lvlStats.healthBoost, health.maxHealth + relic.lvlStats.healthBoost);
            }
            else if (relic.lvlStats.healthBoost < 0){
                health.maxHealth += relic.lvlStats.healthBoost;
                if (health.curHealth > health.maxHealth){
                    health.curHealth = health.maxHealth;
                }
                ItemManager.instance.SetHealth(health.curHealth, health.maxHealth);
            }
            if (relic.baseStats.swordDebuff != Debuffs.None){
                if (meleeDebuffs.TryGetValue(relic.baseStats.swordDebuff.ToString(), out DebuffStats i)){
                    i.duration = i.duration + relic.lvlStats.duration;
                    i.maxStacks = i.maxStacks + relic.lvlStats.maxStacks;
                    meleeDebuffs[relic.baseStats.swordDebuff.ToString()] = i;
                }
            }
            if (relic.baseStats.gunDebuff != Debuffs.None){
                if (gunDebuffs.TryGetValue(relic.baseStats.gunDebuff.ToString(), out DebuffStats i)){
                    i.duration = i.duration + relic.lvlStats.duration;
                    i.maxStacks = i.maxStacks + relic.lvlStats.maxStacks;
                    gunDebuffs[relic.baseStats.gunDebuff.ToString()] = i;
                }
            }
            if (playerRelics[relic.title].curlvl >= relic.maxlvl && !relic.isStoryRelic){
                upgradeables.Remove(relic.title);
            }
        }
        else{
            playerRelics.Add(relic.title, relic);
            Health health = ItemManager.instance.GetHealth();
            if (relic.baseStats.healthBoost > 0){
                ItemManager.instance.SetHealth(health.curHealth + relic.baseStats.healthBoost, health.maxHealth + relic.baseStats.healthBoost);
            }
            else if (relic.baseStats.healthBoost < 0){
                health.maxHealth += relic.baseStats.healthBoost;
                if (health.curHealth > health.maxHealth){
                    health.curHealth = health.maxHealth;
                }
                ItemManager.instance.SetHealth(health.curHealth, health.maxHealth);
            }
            if (relic.curlvl < relic.maxlvl && !relic.isStoryRelic){
                upgradeables.Add(relic.title, relic);
            }
            if (relic.isStoryRelic){
                numbStoryRelics++;
                GameManager.choices.SetFlag(relic.name, 1);
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
            else if (relic.activator == Activators.OnDodge){
                onDodge.Add(relic.title);
            }
            
            totalStats = ItemManager.instance.CombineStats(totalStats, ItemManager.instance.GetItemStats(relic)); 
                
            
            if (relic.baseStats.swordDebuff != Debuffs.None){
                if (meleeDebuffs.TryGetValue(relic.baseStats.swordDebuff.ToString(), out DebuffStats i)){
                    i.duration = i.duration + ItemManager.instance.GetItemStats(relic).duration;
                    i.maxStacks = i.maxStacks + ItemManager.instance.GetItemStats(relic).maxStacks;
                    meleeDebuffs[relic.baseStats.swordDebuff.ToString()] = i;
                }
                else{
                    DebuffStats stats = new DebuffStats{
                        duration = ItemManager.instance.GetItemStats(relic).duration,
                        maxStacks = ItemManager.instance.GetItemStats(relic).maxStacks,
                    };
                    meleeDebuffs.Add(relic.baseStats.swordDebuff.ToString(), stats);             
                }
            }
            if (relic.baseStats.gunDebuff != Debuffs.None){
                if (gunDebuffs.TryGetValue(relic.baseStats.gunDebuff.ToString(), out DebuffStats i)){
                    i.duration = i.duration + ItemManager.instance.GetItemStats(relic).duration;
                    i.maxStacks = i.maxStacks + ItemManager.instance.GetItemStats(relic).maxStacks;
                    gunDebuffs[relic.baseStats.gunDebuff.ToString()] = i;
                }
                else{
                    DebuffStats stats = new DebuffStats{
                        duration = ItemManager.instance.GetItemStats(relic).duration,
                        maxStacks = ItemManager.instance.GetItemStats(relic).maxStacks,
                    };
                    gunDebuffs.Add(relic.baseStats.gunDebuff.ToString(), stats);             
                }
            }
        } 
    }
    public void LoseRelic(RelicScriptableObject relic){
        if (!playerRelics.ContainsKey(relic.title)){
            return;
        }
        Health health = ItemManager.instance.GetHealth();
        ItemStats stats = ItemManager.instance.GetItemStats(relic);
        if (stats.healthBoost < 0){
            ItemManager.instance.SetHealth(health.curHealth - stats.healthBoost, health.maxHealth - stats.healthBoost);
        }
        else if (stats.healthBoost > 0){
            health.maxHealth -= stats.healthBoost;
            if (health.curHealth > health.maxHealth){
                health.curHealth = health.maxHealth;
            }
            ItemManager.instance.SetHealth(health.curHealth, health.maxHealth);
        }
        playerRelics.Remove(relic.title);
        if (relic.isStoryRelic)
        {
            GameManager.choices.SetFlag(relic.name, 0);
        }
        if (upgradeables.ContainsKey(relic.title)){
            upgradeables.Remove(relic.title);
        }
        if (onMelee.Contains(relic.title)){
            onMelee.Remove(relic.title);
        }
        if (onTakeDamage.Contains(relic.title)){
            onTakeDamage.Remove(relic.title);
        }
        if (onKill.Contains(relic.title)){
            onKill.Remove(relic.title);
        }
        if (onDodge.Contains(relic.title)){
            onDodge.Remove(relic.title);
        }
        if (meleeDebuffs.TryGetValue(relic.baseStats.swordDebuff.ToString(), out DebuffStats i)){
            i.duration = i.duration - ItemManager.instance.GetItemStats(relic).duration;
            i.maxStacks = i.maxStacks - ItemManager.instance.GetItemStats(relic).maxStacks;
            if (i.duration <= 0 && i.maxStacks <= 0){
                meleeDebuffs.Remove(relic.baseStats.swordDebuff.ToString());
            }
            else{
                meleeDebuffs[relic.baseStats.swordDebuff.ToString()] = i;
            }
            
        }
        if (gunDebuffs.TryGetValue(relic.baseStats.gunDebuff.ToString(), out DebuffStats y)){
            y.duration = y.duration - ItemManager.instance.GetItemStats(relic).duration;
            y.maxStacks = y.maxStacks - ItemManager.instance.GetItemStats(relic).maxStacks;
            if (y.duration <= 0 && y.maxStacks <= 0){
                gunDebuffs.Remove(relic.baseStats.gunDebuff.ToString());
            }
            else{
                gunDebuffs[relic.baseStats.gunDebuff.ToString()] = y;
            }
            
        }
        totalStats = ItemManager.instance.SubtractStats(totalStats, ItemManager.instance.GetItemStats(relic));

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
        ItemStats stats = ItemManager.instance.GetActivatorStats(relic);
        Debug.Log("CurStacks = " + relic.curStacks + ", MaxStacks = " + stats.maxStacks);
        if (playerRelics[relic.title].curStacks < stats.maxStacks){
            totalStats = ItemManager.instance.CombineStats(totalStats, stats); 
            playerRelics[relic.title].curStacks++;
            Debug.Log(stats.duration);
            yield return new WaitForSeconds(stats.duration);
            Debug.Log(totalStats.swordDamage);
            totalStats = ItemManager.instance.SubtractStats(totalStats, stats);
            Debug.Log(totalStats.swordDamage);
            playerRelics[relic.title].curStacks--;
        }
    }
    public IEnumerator AddEffect(ItemScriptableObject item){
        ItemStats stats = ItemManager.instance.GetItemStats(item);
        totalStats = ItemManager.instance.CombineStats(totalStats,stats);
        yield return new WaitForSeconds(stats.duration);
        totalStats = ItemManager.instance.SubtractStats(totalStats, stats);
    }
}
