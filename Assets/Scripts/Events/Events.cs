using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using DefaultNamespace.OverworldMap;
using MyBox;
using UnityEngine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.InputSystem.Interactions;

[Serializable]
public struct Stats
{
    public int health;
    public int gold;
}

[Serializable]
public class Choice
{
    public string text;
    public bool isTerminal;
    [ConditionalField(nameof(isTerminal))] public RelicCollection relicsToAdd;
    [ConditionalField(nameof(isTerminal))] public RelicCollection relicsToLose;

    [ConditionalField(nameof(isTerminal))] public EventCollection eventsToAdd;
    [ConditionalField(nameof(isTerminal))] public Stats stats;
    [ConditionalField(nameof(isTerminal))] public SceneName nextScene;
    [ConditionalField(nameof(isTerminal))] public int combatIndex;

    [TextArea(7,15)]
    public String followUpText;

    [ConditionalField(nameof(isTerminal), true)]
    public ChoiceIndices nextChoiceIndices;

    [ConditionalField(nameof(isTerminal), true)]
    public bool changeVisuals;

    [ConditionalField(nameof(changeVisuals))]
    public int changeIndex;
}

[Serializable]
public class EventCollection : CollectionWrapper<EventScriptableObject> {}

[Serializable]
public class RelicCollection : CollectionWrapper<RelicScriptableObject> {}

[Serializable]
public class ChoiceIndices : CollectionWrapper<int> {}

[Serializable]
public class ChoiceCollection : CollectionWrapper<Choice>{}

public class Events
{
    private System.Random _random = new System.Random();
    private List<EventScriptableObject> _eventPool = new List<EventScriptableObject>();
    private List<EventScriptableObject> _seedEvents;
    private List<EventScriptableObject> _storyEvents;
    private List<EventScriptableObject> _genericEvents;
    
    public Events(List<EventScriptableObject> seedEvents, List<EventScriptableObject> storyEvents, List<EventScriptableObject> genericEvents)
    {
        _seedEvents = seedEvents;
        _storyEvents = storyEvents;
        int randomEventIndex = _random.Next(_seedEvents.Count);
        _eventPool.Add(_seedEvents[randomEventIndex]);
        // foreach (EventScriptableObject genericEvent in genericEvents)
        // {
        //     _eventPool.Add(genericEvent);
        // }
    }
    
    public EventScriptableObject GetEvent()
    {
        int r = _random.Next(_eventPool.Count);
        EventScriptableObject toReturn = _eventPool[r];
        return toReturn;
    }

    public void RemoveEvent(EventScriptableObject eventToRemove)
    {
        _eventPool.Remove(eventToRemove);
    }
    
    public void AddEvent(EventScriptableObject eventToAdd){
        _eventPool.Add(eventToAdd);
    }

    public bool IsEventPoolEmpty()
    {
        return _eventPool.Count == 0;
    }
}
