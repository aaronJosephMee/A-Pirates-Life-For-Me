using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    Item item;
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
        //if (instance != null){
            instance.GetComponent<InfoWidget>().GiveItem(item);
            //instance.transform.SetParent(this.transform);              
            //instance.transform.localScale = new Vector3(1,1,1);
            Debug.Log(instance.transform.position.x);
            //instance.transform.position += new Vector3(92f,0,0);
            Debug.Log(instance.transform.position.x);
            //instance.transform.localScale = this.transform.localScale;
            
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // if (instance != null){
        //     Destroy(instance);
        //     instance = null;
        // }
    }
}
