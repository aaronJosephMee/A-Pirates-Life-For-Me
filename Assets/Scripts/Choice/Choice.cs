using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

struct Flag{
    public int val;
    public Dictionary<GameObject, Dictionary<int, Func<GameObject, Null>>> dependencies;
}

public class Choices : MonoBehaviour
{
    Choices instance;
    Flag mostRecent;
    Dictionary<String, Flag> flags = new Dictionary<string, Flag>();

    public void AddFlag(String name, int startVal){
        Flag newFlag;
        newFlag.val = startVal;
        newFlag.dependencies = new Dictionary<GameObject, Dictionary<int, Func<GameObject, Null>>>();
        flags.Add(name, newFlag);
    }
    public int CheckFlag(String name){
        Flag val;
        if (!flags.TryGetValue(name, out val)){
            Console.Error.WriteLine("Requested flag doesn't exist");
            return -1;
        }
        return val.val;
    }
    public void CreateDependency(String flag, GameObject gameObject, int onVal, Func<GameObject, Null> whatDo){
        Flag val;
        if (!flags.TryGetValue(flag, out val)){
            Console.Error.WriteLine("Requested flag doesn't exist");
            return;
        }
        Dictionary<int, Func<GameObject, Null>> deps;
        if (!val.dependencies.TryGetValue(gameObject, out deps)){
            deps = new Dictionary<int, Func<GameObject, Null>>();
            val.dependencies.Add(gameObject, deps);
        }
        deps.Add(onVal, whatDo);
    }
    public void SetFlag(String flag, int newVal){
        Flag val;
        if (!flags.TryGetValue(flag, out val)){
            Console.Error.WriteLine("Requested flag doesn't exist");
            return;
        }
        mostRecent = val;
        val.val = newVal;
        Func<GameObject, Null> toDo;
        foreach (KeyValuePair<GameObject, Dictionary<int, Func<GameObject, Null>>> entry in val.dependencies){
            if (entry.Value.TryGetValue(newVal, out toDo)){
                toDo(entry.Key);
            }
        }
    }

}
