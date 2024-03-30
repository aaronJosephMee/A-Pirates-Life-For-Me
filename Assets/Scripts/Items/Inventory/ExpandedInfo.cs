using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpandedInfo : MonoBehaviour
{
    Item item;
    public TextMeshProUGUI nme;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI price;
    public void GiveItem(Item item, bool isShopItem){
        if (item == null){
            item = ItemManager.instance.GenericNoItem;
        }
        this.item = item;
        if (item == ItemManager.instance.GenericNoItem){
            nme.text = "";
        }
        else{
            nme.text = item.title;
            desc.text = item.description;
            price.text = item.price + " Gold";
        }
        price.gameObject.SetActive(isShopItem);
        try{
            if (((ItemScriptableObject)item)){
                nme.color = Color.cyan;
            }
        }
        catch{}
        try{
            if (((RelicScriptableObject)item)){
                nme.color = new Color(255f/255, 218f/255, 1f/255);
            }
        }
        catch{}
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemManager.instance.CurrentGold() < item.price){
            price.color = Color.red;
        }
    }
}
