using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemWidget : InventoryItem,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler
{
    public Color highlight;
    public Color regular;
    public Image background;
    ItemStats stats;
    Health hp;
    // Start is called before the first frame update
    void Start()
    {
        item = ItemManager.instance.playerItems.GetItem();
        stats = ItemManager.instance.GetItemStats(item);
        hp = ItemManager.instance.GetHealth();
        this.GiveItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (item != null && item != ItemManager.instance.GenericNoItem && stats.hpRegen > 0 && hp.curHealth < hp.maxHealth){
            this.background.color = highlight;
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        this.background.color = regular;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item != ItemManager.instance.GenericNoItem && stats.hpRegen > 0 && hp.curHealth < hp.maxHealth){
            hp.curHealth = hp.curHealth + (stats.duration * stats.hpRegen);
            if (hp.curHealth > hp.maxHealth){
                hp.curHealth = hp.maxHealth;
                this.background.color = regular;
            }
            ItemManager.instance.SetHealth(hp.curHealth, hp.maxHealth);
            ItemManager.instance.playerItems.itemUses++;
            if (ItemManager.instance.playerItems.itemUses >= ((ItemScriptableObject)item).uses){
                ItemManager.instance.SetItem(null);
                this.GiveItem(null);
            }
            base.DestroyWidget();
            if (item != null && item != ItemManager.instance.GenericNoItem){
                this.OnPointerEnter(eventData);
            }
            else{
                this.background.color = regular;
            }
            
        }
    }
}
