using System;
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
        statText += "Pellet Count: " + stats.bulletCount + "\n";
        statText += "Damage Reduction: " + stats.defense + "%    ";
        statText += "Ricochets: " + stats.richochet + "\n";
        statText += "Fire Rate: " + stats.fireRate + "      ";
        statText += "Speed: " + stats.speedBoost +"    ";
        // statText += "Bullet Size: " + stats.projectileSize + "       ";
        // statText += "Accuracy: " + stats.accuracy + "\n";
        statText += "Dodge Chance: " + MathF.Round(stats.dodgeChance*100,2) + "%\n";
        statText += "Crit Chance: " + MathF.Round(stats.critChance*100,2) + "%       ";
        statText += "Crit Multiplier: " + stats.critMultiplier + "x\n";
        text.text = statText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
