using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Health hp = ItemManager.instance.GetHealth();
        text.text = "Health: " + MathF.Round(hp.curHealth, 2) + "/" + MathF.Round(hp.maxHealth, 2);
    }
}
