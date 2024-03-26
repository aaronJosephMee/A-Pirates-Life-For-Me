using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RelicScriptableObject", order = 1)]
public class RelicScriptableObject :  Item
{
    public Activators activator;
    public ItemStats ActivatorStats;
    public ItemStats Activatorlvl;
    [System.NonSerialized] public int curStacks;
}

public enum Activators{
    [Description("Passive")]
    Passive,
    [Description("Melee")]
    Melee,
    [Description("On Kill")]
    OnKill,
    [Description("Take Damage")]
    OnTakeDamage,
}
