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
    public TextMeshProUGUI desc;
    public TextMeshProUGUI expandedName;
    public GameObject nameAndDesc;
    public GameObject widget;
    private GameObject instance;
    public bool ownedItem = false;

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
            expandedName.text = item.name;
            desc.text = item.description;
        }
        nameAndDesc.SetActive(false);
        if (item.image != null){
            image.sprite = item.image;
        }
    }  
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != ItemManager.instance.GenericNoItem){
            nameAndDesc.SetActive(true);
            instance = Instantiate(widget, this.transform.parent);
            instance.GetComponent<InfoWidget>().ownedItem = ownedItem;
            instance.GetComponent<InfoWidget>().GiveItem(item);
            if (shopUpgrade){
                instance.GetComponent<InfoWidget>().SetTextLevel();
            }
            else{
                instance.GetComponent<InfoWidget>().SetTextTotal();
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyWidget();
        nameAndDesc.SetActive(false);
    }
    public void DestroyWidget(){
        if (instance != null){
            Destroy(instance);
            instance = null;
        }
    }
}
