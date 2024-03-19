using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public ShopItem[] shopItems;
    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        Item[] items = new Item[shopItems.Length];
        for (int i = 0; i < items.Length; i++){
            int res = random.Next(100);
            if (res < 10){
                res = random.Next(100);
                if (res < 50 && (ItemManager.instance.CurrentWeapon().curlvl < ItemManager.instance.CurrentWeapon().maxlvl)){
                    items[i] = ItemManager.instance.CurrentWeapon();
                }
                else{
                    items[i] = ItemManager.instance.GetRandWeapon();                    
                }
                continue;
            }
            else if (res < 30){
                items[i] = ItemManager.instance.GetRandItem();
                continue;
            }
            else{
                int threshold = 100;
                if (ItemManager.instance.GetRandRelic() != null){
                    if (ItemManager.instance.RelicCount() < ItemManager.instance.MaxRelics/4){
                        threshold = 10;
                    }
                    else if (ItemManager.instance.RelicCount() < ItemManager.instance.MaxRelics/2){
                        threshold = 30;
                    }
                    else if (ItemManager.instance.RelicCount() < ItemManager.instance.MaxRelics){
                        threshold = 60;
                    }
                }
                res = random.Next(100);
                if (res < threshold){
                    items[i] = ItemManager.instance.GetRandUpgrade();
                    if (items[i] == null){
                        items[i] = ItemManager.instance.GetRandRelic();
                    }
                }
                else{
                    items[i] = ItemManager.instance.GetRandRelic();
                }
            }
        }
        for (int i = 0; i < items.Length; i++){
            shopItems[i].GiveItem(items[i]);
        }
    }


}
