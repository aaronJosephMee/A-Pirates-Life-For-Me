using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum Type{
    [Description("Single Shot")]
    SingleShot,
    [Description("Melee")]
    Melee,
}
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
public class WeaponScriptableObject : Item
{
    public Type type;
}
