using DefaultNamespace.OverworldMap;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EventScriptableObject", order = 2)]
public class EventScriptableObject : ScriptableObject
{
    public string name;
    public SceneName scene;
    public int sceneIdx;
    [TextArea(7,15)]
    public string flavorText;
    public bool isMinigame;
    public Choice[] choices;
}