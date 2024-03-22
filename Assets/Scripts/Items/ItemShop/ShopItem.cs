using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler
{
    public Item item;
    public TextMeshProUGUI price;
    public Image background;
    public Color highlight;
    public Color regular;
    [System.NonSerialized] public InventoryItem inventoryItem;
    public bool free = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(free);
        if (item != null && item != ItemManager.instance.GenericNoItem && (ItemManager.instance.CurrentGold() >= item.price || free)){
            this.background.color = highlight;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.background.color = regular;
    }

    // Start is called before the first frame update
    void Awake()
    {
        inventoryItem = this.GetComponent<InventoryItem>();
    }
    public virtual void GiveItem(Item item){
        if (item == null){
            item = ItemManager.instance.GenericNoItem;
        }
        this.item = item;
        if (item == ItemManager.instance.GenericNoItem && price != null){
            price.text = "";
        }
        else if (price != null){
            price.text = item.price + " Gold";
        }
        
        if (ItemManager.instance.IsUpgrade(item)){
            inventoryItem.shopUpgrade = true;
        }
        inventoryItem.GiveItem(item);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item != ItemManager.instance.GenericNoItem && (ItemManager.instance.CurrentGold() >= item.price || free)){
            if (!free)
            {
                ItemManager.instance.AddGold(-item.price);
            }
            try{
                ItemManager.instance.AddRelic((RelicScriptableObject)item);
            }
            catch{}
            try{
                ItemManager.instance.AddWeapon((WeaponScriptableObject)item);
            }
            catch{}
            try{
                ItemManager.instance.SetItem((ItemScriptableObject)item);
            }
            catch{}
            this.GiveItem(ItemManager.instance.GenericNoItem);
            this.background.color = regular;
            inventoryItem.DestroyWidget();
        }
    }
    void Update(){
        if (item != null && !ItemManager.instance.IsUpgrade(item) && inventoryItem.shopUpgrade){
            GiveItem(ItemManager.instance.GenericNoItem);
        }
        if (item != null && ItemManager.instance.IsUpgrade(item) && !inventoryItem.shopUpgrade){
            GiveItem(this.item);
        }
    }
}
