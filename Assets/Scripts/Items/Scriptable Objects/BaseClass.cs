using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject{
    public Sprite image;
    public string title;
    public int maxlvl;
    [System.NonSerialized] public int curlvl = 1;
    public ItemStats baseStats;
    public ItemStats lvlStats;
    public int price;
}
