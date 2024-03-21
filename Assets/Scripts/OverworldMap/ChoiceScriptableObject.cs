using DefaultNamespace.OverworldMap;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ChoiceScriptableObject", order = 1)]
public class ChoiceScriptableObject : ScriptableObject
{
    public ChoiceType choiceType;
    public Sprite sprite;
    public GameObject icon;
}