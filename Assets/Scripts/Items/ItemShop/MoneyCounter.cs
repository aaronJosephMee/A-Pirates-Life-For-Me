using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _textMeshProUGUI.text = "Current Money: " + ItemManager.instance.CurrentGold();
    }

    // Update is called once per frame
    void Update()
    {
        _textMeshProUGUI.text = "Current Money: " + ItemManager.instance.CurrentGold();
    }
}
