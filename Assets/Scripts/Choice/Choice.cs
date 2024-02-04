using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Flag{
    public int val;
    public Dictionary<GameObject, List<int>> dependencies;
}

public class Choices : MonoBehaviour
{
    Choices instance;
    Flag mostRecent;
    Dictionary<String, Flag> flags;

    public void AddFlag(String name, int startVal){
        Flag newFlag;
        newFlag.val = startVal;
        newFlag.dependencies = new Dictionary<GameObject, List<int>>();
        flags.Add(name, newFlag);
    }
    public int CheckFlag(String name){
        Flag val;
        if (!flags.TryGetValue(name, out val)){
            Console.Error.WriteLine("Requested flag doesn't exist");
        }
        return val.val;
    }
    public void CreateDependency(String flag, GameObject gameObject, int onVal){
        Flag val;
        if (!flags.TryGetValue(name, out val)){
            Console.Error.WriteLine("Requested flag doesn't exist");
        }
        List<int> deps;
        if (!val.dependencies.TryGetValue(gameObject, out deps)){
            deps = new List<int>();
            val.dependencies.Add(gameObject, deps);
        }
        deps.Add(onVal);
    }
    public void SetFlag(String flag,)

}
