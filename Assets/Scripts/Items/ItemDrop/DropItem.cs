using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : ShopItem
{
    public override void GiveItem(Item item){
        this.free = true;
        if (item == null){
            item = ItemManager.instance.GenericNoItem;
        }
        this.item = item;
        if (ItemManager.instance.IsUpgrade(item)){
            inventoryItem.shopUpgrade = true;
        }
        
        inventoryItem.GiveItem(item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item != ItemManager.instance.GenericNoItem){
            base.OnPointerDown(eventData);
            OverworldMapManager.Instance.TransitionBackToMap();
            Destroy(this.transform.parent.parent.parent.gameObject);
        }
    }
}
