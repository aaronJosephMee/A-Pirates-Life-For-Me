using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]

public class ItemScriptableObject : Item
{
    public int uses;
    public float cooldown;
}
