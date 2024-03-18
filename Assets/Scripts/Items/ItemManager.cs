using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public PlayerItems playerItems = new PlayerItems();
    public ItemPool itemPool;
    public RelicScriptableObject[] relics;
    public ItemScriptableObject[] items;
    public WeaponScriptableObject[] weapons;
    public WeaponScriptableObject defaultWeapon;
    public ItemScriptableObject defaultItem;
    void Awake(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            itemPool = new ItemPool(relics, items, weapons);
            playerItems.AddWeapon(defaultWeapon);
            playerItems.SetWeapon(defaultWeapon.title);
            playerItems.SetItem(defaultItem);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
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
