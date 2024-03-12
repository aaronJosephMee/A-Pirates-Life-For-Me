using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    Item item;
    TextMeshProUGUI nme;
    public GameObject widget;
    private GameObject instance;
    private void Start() {
        nme = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    public void GiveItem(Item item){
        this.item = item;
        nme.text = item.name;
        
    }  
    public void OnPointerEnter(PointerEventData eventData)
    {
        instance = Instantiate(widget);
        instance.GetComponent<InfoWidget>().GiveItem(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(instance);
    }
}
