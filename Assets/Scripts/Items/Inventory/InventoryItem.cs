using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public Item item;
    public TextMeshProUGUI nme;
    public GameObject widget;
    private GameObject instance;
    public Vector3 offset;

    // Start is called before the first frame update
    public void GiveItem(Item item){
        this.item = item;
        nme.text = item.name;
    }  
    public void OnPointerEnter(PointerEventData eventData)
    {
        instance = Instantiate(widget, this.transform);
        instance.GetComponent<InfoWidget>().GiveItem(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (instance != null){
            Destroy(instance);
            instance = null;
        }
    }
}
