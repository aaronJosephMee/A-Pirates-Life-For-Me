using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public RelicScriptableObject[] relics;
    public EventScriptableObject[] eventsToAdd;
    public Stats stats;
}

public class Events
{
    private System.Random _random = new System.Random();
    string[] _allStats = new string[] {"Health", "Gold"};
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
        foreach (EventScriptableObject genericEvent in genericEvents)
        {
            _eventPool.Add(genericEvent);
        }
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
