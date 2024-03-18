using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    public TextMeshProUGUI gold;
    public Button open;
    public Button getItem;
    public Button getMoney;
    public Button openShop;
    public GameObject inventory;
    public GameObject shop;
    public int goldToAdd;
    // Start is called before the first frame update
    void Start()
    {
        open.onClick.AddListener(OpenInv);
        getItem.onClick.AddListener(GetItem);
        openShop.onClick.AddListener(OpenShop);
        getMoney.onClick.AddListener(AddGold);
    }
    void Update(){
        gold.text = "Gold: " + ItemManager.instance.CurrentGold();
    }
    void OpenInv(){
        Instantiate(inventory);
    }
    void GetItem(){
        ItemManager.instance.playerItems.AddRelic((RelicScriptableObject)ItemManager.instance.itemPool.GetRandItem("Relic"));
    }
    void OpenShop(){
        Instantiate(shop);
    }
    void AddGold(){
        ItemManager.instance.AddGold(goldToAdd);
    }
}
