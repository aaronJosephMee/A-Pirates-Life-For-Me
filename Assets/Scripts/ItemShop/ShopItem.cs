using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Image background;
    public Color highlight;
    public Color regular;
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.background.color = highlight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.background.color = regular;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
