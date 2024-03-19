using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RelicScriptableObject", order = 1)]
public class RelicScriptableObject :  Item
{
    public Activators activator;
    [System.NonSerialized] public int curStacks;
}

public enum Activators{
    [Description("Passive")]
    Passive,
    [Description("Melee")]
    Melee,
    [Description("On Kill")]
    OnKill,
    [Description("On Take Damage")]
    OnTakeDamage,
}
