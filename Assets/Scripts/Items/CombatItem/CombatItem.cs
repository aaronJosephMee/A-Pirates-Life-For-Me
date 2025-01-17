using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] TextMeshProUGUI charges;
    Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        item = ItemManager.instance.CurrentItem();
        oldItem = ItemManager.instance.CurrentItem();
        if (item == null){
            item = ItemManager.instance.GenericNoItem;
            useImage.gameObject.SetActive(false);
            charges.text = "";
        }
        else{
            charges.text = "" + (((ItemScriptableObject) item).uses - ItemManager.instance.GetItemUses());
        }
        if (item.image != null){
            itemImage.sprite = item.image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useButton) && item != ItemManager.instance.GenericNoItem && coolDownImage.transform.localScale.x <= 0 && activeImage.transform.localScale.x <= 0 && !combatManager.Instance.waveCleared && !player.isDead){
            ItemManager.instance.UseItem();
            item = ItemManager.instance.CurrentItem();
            if (item == null){
                item = ItemManager.instance.GenericNoItem;
                charges.text = "";
            }
            else{
                charges.text = "" + (((ItemScriptableObject) item).uses - ItemManager.instance.GetItemUses());
            }  
            if (item != ItemManager.instance.GenericNoItem && ((ItemScriptableObject)item).cooldown != 0){
                StartCoroutine(StartCooldown(item.baseStats.duration));
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
        yield return new WaitForSeconds(sec - 0.05f);
        coolDownImage.transform.localScale = new Vector3(1,1,1);
    }
}
