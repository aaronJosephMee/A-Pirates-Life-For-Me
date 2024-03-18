using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWidget : InventoryItem
{
    // Start is called before the first frame update
    void Start()
    {
        item = ItemManager.instance.playerItems.GetItem();
        this.GiveItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
