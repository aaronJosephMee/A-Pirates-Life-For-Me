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
        Dictionary<string, RelicScriptableObject> allRelics = ItemManager.instance.GetRelics();
        List<string> keys = new List<string>(allRelics.Keys);
        GameObject[] instances = new GameObject[relics];
        for (int i = 0; i < relics; i++){
            GameObject instance  = Instantiate(inventoryItem, this.transform.position + offset, Quaternion.identity);
            instances[i] = instance;
            instance.GetComponentInChildren<InventoryItem>().GiveItem(allRelics[keys[i]]);
            if (offset.x < maxlen){
                offset = offset + new Vector3(horizontalGap,0,0);
            }
            else{
                offset = new Vector3(0,verticalGap + offset.y, 0);
            }

        }
        for(int i = relics - 1; i >= 0; i--){
            instances[i].transform.SetParent(this.transform.parent);
            instances[i].transform.localScale = this.transform.localScale;
        }
    }
}
