using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSpawner : MonoBehaviour
{
    public int maxlen;
    public int horizontalGap;
    public int verticalGap;
    public GameObject inventoryItem;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 offset = new Vector3(0,0,0);
        int relics = ItemManager.instance.playerItems.RelicCount();
        Dictionary<string, Item> allRelics = ItemManager.instance.playerItems.GetRelics();
        List<string> keys = new List<string>(allRelics.Keys);
        for (int i = 0; i < relics; i++){
            GameObject instance  = Instantiate(inventoryItem, this.transform.position + offset, Quaternion.identity);
            instance.transform.SetParent(this.transform.parent);
            instance.GetComponent<InventoryItem>().GiveItem(allRelics[keys[i]]);
            if (offset.x < maxlen){
                offset = offset + new Vector3(horizontalGap,0,0);
            }
            else{
                offset = new Vector3(0,verticalGap + offset.y, 0);
            }

        }
    }
}
