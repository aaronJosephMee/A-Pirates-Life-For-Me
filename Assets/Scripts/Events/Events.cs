using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.InputSystem.Interactions;

public struct Relic
{
    public string name;
}
public struct Stats
{
    public int health;
    public int gold;
}

public struct Choice
{
    public string text;
    public Relic[] relics;
    public string[] eventsToAdd;
    public Stats stats;
}
public struct Event
{
    public string name;
    public string scene;
    public int sceneIdx;
    public string flavorText;
    public bool isMinigame;
    public string minigame;
    public Choice[] choices;
    
}

public class Events
{
    System.Random random = new System.Random();
    string[] allStats = new string[] {"Health", "Gold"};
    List<Event> eventPool = new List<Event>();
    Dictionary<String, Event> seedEvents;
    Dictionary<String, Event> storyEvents;
    Dictionary<String, Event> genericEvents;
    public Events()
    {
        seedEvents = LoadEvents("SeedEvents");
        storyEvents = LoadEvents("StoryEvents");
        List<string> startSeeds = new List<string>(seedEvents.Keys);
        int r = random.Next(startSeeds.Count);
        eventPool.Add(seedEvents[startSeeds[r]]);
        //Debug.Log("Seeded event: " + eventPool[0].name);
    }
    public Event GetEvent()
    {
        int r = random.Next(eventPool.Count);
        Event toReturn = eventPool[r];
        eventPool.Remove(eventPool[r]);
        return toReturn;
    }
    public void AddEvent(string eventToAdd){
        eventPool.Add(storyEvents[eventToAdd]);
    }
    Dictionary<String, Event> LoadEvents(String filename)
    {
        int i = 0;
        Dictionary<String,Event> result = new Dictionary<string, Event>();
        String[] events = File.ReadAllLines(Application.dataPath + "/Events/" + filename);
        while (i < events.Length){
            string internalName = events[i];
            i++;
            Event evt = new Event();
            evt.name = events[i];
            i++;
            evt.flavorText = events[i];
            i++;
            evt.scene = events[i];
            i++;
            evt.sceneIdx = int.Parse(events[i]);
            i++;
            evt.isMinigame = bool.Parse(events[i]);
            i++;
            if (evt.isMinigame){
                evt.minigame = events[i];
                i++;
                result.Add(internalName, evt);
                continue;
            }
            evt.choices = new Choice[int.Parse(events[i])];
            i++;
            for (int y = 0; y < evt.choices.Length; y++){
                Choice c = new Choice();
                c.text = events[i];
                i++;
                foreach (string stat in allStats){
                    switch(events[i].Split(" ")[0]){
                        case "Health":
                            c.stats.health = int.Parse(events[i].Split(" ")[1]);
                            i++;
                            break;
                        case "Gold":
                            c.stats.gold = int.Parse(events[i].Split(" ")[1]);
                            i++;
                            break;
                        default:
                            break;
                    }
                }
                c.relics = new Relic[int.Parse(events[i])];
                i++;
                for (int x = 0; x<c.relics.Length;x++){
                    Relic r = new Relic();
                    r.name = events[i];
                    i++;
                    c.relics[x] = r;
                }
                c.eventsToAdd = new string[int.Parse(events[i])];
                i++;
                for (int x = 0; x<c.eventsToAdd.Length;x++){
                    c.eventsToAdd[x] = events[i];
                    i++;
                }
                evt.choices[y] = c;
            }
            result.Add(internalName, evt);

        }
        return result;
    
    }
    void DebugPrint(Dictionary<String, Event> target)
    {
        foreach(KeyValuePair<String, Event> entry in target){
            Debug.Log("Iname: " + entry.Key + ", Name:" + entry.Value.name + ", Text: " + entry.Value.flavorText);
        }

    }
}
