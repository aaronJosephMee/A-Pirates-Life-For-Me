using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EventScriptableObject", order = 2)]
public class EventScriptableObject : ScriptableObject
{
    public string title;
    public SceneName scene;
    public int sceneIdx;
    [TextArea(7,15)]
    public string flavorText;
    public bool isMinigame;
    public int[] initialChoices;
    public ChoiceCollection choices;
}