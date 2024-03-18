using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWidget : InventoryItem
{
    Item weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = ItemManager.instance.playerItems.GetWeapon();
        this.GiveItem(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
