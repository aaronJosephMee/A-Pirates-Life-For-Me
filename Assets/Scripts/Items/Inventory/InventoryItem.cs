using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public bool shopItem = false;
    public Image image;
    public Item item;
    public TextMeshProUGUI nme;
    public GameObject widget;
    private GameObject instance;
    public Vector3 offset;

    // Start is called before the first frame update
    public void GiveItem(Item item){
        this.item = item;
        nme.text = item.name;
        if (item.image != null){
            image.sprite = item.image;
        }
    }  
    public void OnPointerEnter(PointerEventData eventData)
    {
        instance = Instantiate(widget, this.transform.parent);
        instance.GetComponent<InfoWidget>().GiveItem(item);
        instance.GetComponent<InfoWidget>().SetTextTotal();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (instance != null){
            Destroy(instance);
            instance = null;
        }
    }
}
