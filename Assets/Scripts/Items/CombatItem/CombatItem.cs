using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatItem : MonoBehaviour
{
    [SerializeField] Image itemImage;
    Item item;
    KeyCode useButton = KeyCode.E;
    [SerializeField] GameObject coolDownImage;
    void Awake()
    {
        item = ItemManager.instance.CurrentItem();
        if (item == null){
            item = ItemManager.instance.GenericNoItem;
        }
        if (item.image != null){
            itemImage.sprite = item.image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useButton) && item != ItemManager.instance.GenericNoItem && coolDownImage.transform.localScale.x <= 0){
            ItemManager.instance.UseItem();
            item = ItemManager.instance.CurrentItem();
            if (item == null){
                item = ItemManager.instance.GenericNoItem;
            }
            if (item.image != null){
                itemImage.sprite = item.image;
            }
            if (item != ItemManager.instance.GenericNoItem && ((ItemScriptableObject)item).cooldown != 0){
                coolDownImage.transform.localScale = new Vector3(1,1,1);
            }
        }
        if (coolDownImage.transform.localScale.x > 0){
            coolDownImage.transform.localScale = new Vector3(coolDownImage.transform.localScale.x - Time.deltaTime/((ItemScriptableObject)item).cooldown,1,1);
            if (coolDownImage.transform.localScale.x < 0){
                coolDownImage.transform.localScale = new Vector3(0,1,1);
            }
        }
    }
}
