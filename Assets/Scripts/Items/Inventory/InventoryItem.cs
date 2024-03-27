using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [System.NonSerialized] public bool shopUpgrade = false;
    public Image image;
    public Item item;
    public TextMeshProUGUI nme;
    public GameObject widget;
    public GameObject nameAndDesc;
    private GameObject instance;
    private GameObject instanceDesc;
    public bool ownedItem = false;
    public bool isShopItem;

    // Start is called before the first frame update
    public void GiveItem(Item item){
        if (item == null){
            item = ItemManager.instance.GenericNoItem;
        }
        this.item = item;
        if (item == ItemManager.instance.GenericNoItem){
            nme.text = "";
        }
        else{
            nme.text = item.name;
        }
        if (item.image != null){
            image.sprite = item.image;
        }
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
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != ItemManager.instance.GenericNoItem){
            instance = Instantiate(widget, this.transform.parent);
            instance.GetComponent<InfoWidget>().ownedItem = ownedItem;
            instance.GetComponent<InfoWidget>().GiveItem(item);
            if (shopUpgrade){
                instance.GetComponent<InfoWidget>().SetTextLevel();
            }
            else{
                instance.GetComponent<InfoWidget>().SetTextTotal();
            }
            instanceDesc = Instantiate(nameAndDesc, this.transform.parent);
            instanceDesc.GetComponent<ExpandedInfo>().GiveItem(item, isShopItem);
        }
        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        DestroyWidget();
    }
    public void DestroyWidget(){
        if (instance != null){
            Destroy(instance);
            instance = null;
        }
        if (instanceDesc != null){
            Destroy(instanceDesc);
            instanceDesc = null;
        }
    }
}
