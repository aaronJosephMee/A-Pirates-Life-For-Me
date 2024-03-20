using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsText : MonoBehaviour
{
    TextMeshProUGUI text;
    ItemStats stats;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        stats = ItemManager.instance.playerItems.TotalStats();
        string statText = "";
        statText += "Damage: " + stats.gunDamage + "\n";
        statText += "Defense: " + stats.defense + "\n";
        text.text = statText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
