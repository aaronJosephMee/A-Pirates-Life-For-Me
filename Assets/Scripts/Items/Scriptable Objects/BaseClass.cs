using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Generic", order = 1)]
public class Item : ScriptableObject{
    public Sprite image;
    public string title;
    public string description;
    public int maxlvl;
    [System.NonSerialized] public int curlvl = 1;
    public ItemStats baseStats;
    public ItemStats lvlStats;
    public int price;
    
}
