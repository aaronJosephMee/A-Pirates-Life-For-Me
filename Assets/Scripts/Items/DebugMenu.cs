using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    public Button open;
    public Button getItem;
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        open.onClick.AddListener(OpenInv);
        getItem.onClick.AddListener(GetItem);
    }

    void OpenInv(){
        Instantiate(inventory);
    }
    void GetItem(){
        ItemManager.instance.playerItems.AddRelic((RelicScriptableObject)ItemManager.instance.itemPool.GetRandItem("Relic"));
    }
}
