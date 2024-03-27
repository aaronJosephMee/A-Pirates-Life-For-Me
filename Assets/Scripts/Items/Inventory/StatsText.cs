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
        statText += "Fire Rate: " + stats.fireRate + "      ";
        statText += "Ricochets: " + stats.richochet + "\n";
        // statText += "Bullet Size: " + stats.projectileSize + "       ";
        // statText += "Accuracy: " + stats.accuracy + "\n";
        statText += "Dodge Chance: " + stats.dodgeChance + "       ";
        statText += "Crit Chance: " + stats.critChance + "       ";
        statText += "Crit Multiplier: " + stats.critMultiplier + "\n";
        text.text = statText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
