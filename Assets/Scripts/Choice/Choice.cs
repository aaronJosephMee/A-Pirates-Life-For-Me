using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

struct Flag{
    public int val;
    public Dictionary<GameObject, Dictionary<int, Func<GameObject, int>>> dependencies;
}

public class Choices
{
    Choices instance;
    Flag mostRecent;
    Dictionary<String, Flag> flags = new Dictionary<string, Flag>();

    public void AddFlag(String name, int startVal){
        Flag newFlag;
        newFlag.val = startVal;
        newFlag.dependencies = new Dictionary<GameObject, Dictionary<int, Func<GameObject, int>>>();
        Debug.Log("Adding " + name);
        flags.Add(name, newFlag);
    }
    public int CheckFlag(String name){
        Flag val;
        if (!flags.TryGetValue(name, out val)){
            //Debug.Log("Requested flag doesn't exist");
            return -1;
        }
        return val.val;
    }
    public void CreateDependency(String flag, GameObject gameObject, int onVal, Func<GameObject, int> whatDo){
        Flag val;
        Debug.Log("In dep");
        if (!flags.TryGetValue(flag, out val)){
            Debug.Log("Requested flag doesn't exist");
            return;
        }
        Debug.Log("In dep");
        Dictionary<int, Func<GameObject, int>> deps;
        if (!val.dependencies.TryGetValue(gameObject, out deps)){
            deps = new Dictionary<int, Func<GameObject, int>>();
            val.dependencies.Add(gameObject, deps);
        }
        deps.Add(onVal, whatDo);
    }
    public void SetFlag(String flag, int newVal){
        Debug.Log("Setting " + flag + " to " + newVal);
        Flag val;
        if (!flags.TryGetValue(flag, out val)){
            Debug.Log("Requested flag doesn't exist");
            return;
        }
        
        mostRecent = val;
        val.val = newVal;
        Func<GameObject, int> toDo;
        foreach (KeyValuePair<GameObject, Dictionary<int, Func<GameObject, int>>> entry in val.dependencies){
            Debug.Log("calling dependency");
            if (entry.Value.TryGetValue(newVal, out toDo)){
                
                toDo(entry.Key);
            }
        }
    }

}
