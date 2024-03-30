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
        statText += "Gun Damage: " + stats.gunDamage + "   ";
        statText += "Sword Damage: " + stats.swordDamage + "   ";
        statText += "Defense: " + stats.defense + "\n";
        statText += "Bullet Count: " + stats.bulletCount + "     ";
        
        statText += "Ricochets: " + stats.richochet + "\n";
        statText += "Fire Rate: " + stats.fireRate + "      ";
        // statText += "Bullet Size: " + stats.projectileSize + "       ";
        // statText += "Accuracy: " + stats.accuracy + "\n";
        statText += "Dodge Chance: " + stats.dodgeChance*100 + "%\n";
        statText += "Crit Chance: " + stats.critChance*100 + "%       ";
        statText += "Crit Multiplier: " + stats.critMultiplier + "x\n";
        text.text = statText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
