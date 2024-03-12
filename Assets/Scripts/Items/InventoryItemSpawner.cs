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
        for (int i = 0; i < relics; i++){
            Instantiate(inventoryItem, this.transform.position + offset, Quaternion.identity);
            if (offset.x < maxlen){
                offset = offset + new Vector3(horizontalGap,0,0);
            }
            else{
                offset = new Vector3(0,verticalGap + offset.y, 0);
            }

        }
    }
}
