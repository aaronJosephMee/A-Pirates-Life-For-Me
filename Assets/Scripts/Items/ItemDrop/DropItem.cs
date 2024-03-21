using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : ShopItem
{
    public override void GiveItem(Item item){
        if (item == null){
            item = ItemManager.instance.GenericNoItem;
        }
        this.item = item;
        if (ItemManager.instance.IsUpgrade(item)){
            inventoryItem.shopUpgrade = true;
        }

        int preveiousPrice = item.price;
        item.price = 0;
        inventoryItem.GiveItem(item);
        item.price = preveiousPrice;
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
