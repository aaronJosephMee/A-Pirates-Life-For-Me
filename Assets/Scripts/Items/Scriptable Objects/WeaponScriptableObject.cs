using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type{
    SingleShot,
    Melee,
}
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
public class WeaponScriptableObject : Item
{
    public Type type;
}
