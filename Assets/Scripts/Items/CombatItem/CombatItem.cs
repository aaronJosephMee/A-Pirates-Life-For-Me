using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatItem : MonoBehaviour
{
    [SerializeField] Image itemImage;
    Item item;
    Item oldItem;
    KeyCode useButton = KeyCode.E;
    [SerializeField] GameObject coolDownImage;
    [SerializeField] GameObject activeImage;
    [SerializeField] GameObject useImage;
    void Awake()
    {
        item = ItemManager.instance.CurrentItem();
        oldItem = ItemManager.instance.CurrentItem();
        if (item == null){
            item = ItemManager.instance.GenericNoItem;
            useImage.gameObject.SetActive(false);
        }
        if (item.image != null){
            itemImage.sprite = item.image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useButton) && item != ItemManager.instance.GenericNoItem && coolDownImage.transform.localScale.x <= 0 && activeImage.transform.localScale.x <= 0 && !combatManager.Instance.waveCleared){
            ItemManager.instance.UseItem();
            item = ItemManager.instance.CurrentItem();
            if (item == null){
                item = ItemManager.instance.GenericNoItem;
            }
            if (item != ItemManager.instance.GenericNoItem && ((ItemScriptableObject)item).cooldown != 0){
                StartCoroutine(StartCooldown(((ItemScriptableObject)item).cooldown));
            }
            activeImage.transform.localScale = new Vector3(1,1,1);
            useImage.SetActive(false);
        }
        if (coolDownImage.transform.localScale.x > 0){
            coolDownImage.transform.localScale = new Vector3(coolDownImage.transform.localScale.x - Time.deltaTime/((ItemScriptableObject)item).cooldown,1,1);
            if (coolDownImage.transform.localScale.x < 0){
                coolDownImage.transform.localScale = new Vector3(0,1,1);
            }
        }
        if (activeImage.transform.localScale.x > 0){
            activeImage.transform.localScale = new Vector3(activeImage.transform.localScale.x - Time.deltaTime/ItemManager.instance.GetItemStats(oldItem).duration,1,1);
            if (activeImage.transform.localScale.x < 0){
                activeImage.transform.localScale = new Vector3(0,1,1);
            }
        }
        if (item != ItemManager.instance.GenericNoItem && coolDownImage.transform.localScale.x <= 0 && activeImage.transform.localScale.x <= 0){
            useImage.SetActive(true);
        }
        if (item.image != null && coolDownImage.transform.localScale.x <= 0 && activeImage.transform.localScale.x <= 0){
                itemImage.sprite = item.image;
            }
    }
    IEnumerator StartCooldown(float sec){
        yield return new WaitForSeconds(sec - 0.01f);
        coolDownImage.transform.localScale = new Vector3(1,1,1);
    }
}
