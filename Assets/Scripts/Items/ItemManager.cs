using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public PlayerItems playerItems = new PlayerItems();
    public ItemPool itemPool = new ItemPool();
    void Awake(){
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
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
    public ItemStats CombineStats(ItemStats IS1, ItemStats IS2){
        IS1.duration += IS2.duration;
        IS1.defense += IS2.defense;
        IS1.damage += IS2.damage;
        return IS1;
    }
}
