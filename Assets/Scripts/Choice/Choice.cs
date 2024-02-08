using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

struct Dependency{
    public Dictionary<GameObject, Dictionary<int, Func<GameObject, int>>> deps;
}
public class Choices
{
    Dictionary<String, int> flags = new Dictionary<string, int>();

    Dictionary<String, Dependency> dependencies = new Dictionary<string, Dependency>();

    public void AddFlag(String name, int startVal){
        int newFlag;
        newFlag = startVal;
        flags.Add(name, newFlag);
    }

    public int CheckFlag(String name){
        int val;
        if (!flags.TryGetValue(name, out val)){
            Debug.Log("Requested flag doesn't exist");
            return -1;
        }
        return val;
    }

    public void CreateDependency(String flag, GameObject gameObject, int onVal, Func<GameObject, int> whatDo){
        int state;
        if (!flags.TryGetValue(flag, out state)){
            Debug.Log("Requested flag doesn't exist");
            return;
        }
        Dependency val;
        if (!dependencies.TryGetValue(flag, out val)){
            val = new Dependency();
            val.deps = new Dictionary<GameObject, Dictionary<int, Func<GameObject, int>>>();
            dependencies.Add(flag, val);
        }
        Dictionary<int, Func<GameObject, int>> deps;
        if (!val.deps.TryGetValue(gameObject, out deps)){
            deps = new Dictionary<int, Func<GameObject, int>>();
            val.deps.Add(gameObject, deps);
        }
        deps.Add(onVal, whatDo);
        val.deps[gameObject] = deps;
        if (state == onVal){
            whatDo(gameObject);
        }
    }

    public void SetFlag(String flag, int newVal){
        int val;
        if (!flags.TryGetValue(flag, out val)){
            Debug.Log("Requested flag doesn't exist");
            return;
        }
        val = newVal;
        flags[flag] = val;
        Dependency dep;
        Func<GameObject, int> toDo;
        if (dependencies.TryGetValue(flag, out dep)){
            foreach (KeyValuePair<GameObject, Dictionary<int, Func<GameObject, int>>> entry in dep.deps){
                if (entry.Value.TryGetValue(newVal, out toDo)){   
                    toDo(entry.Key);
                }
            }
        }
        
    }
    
    public void RemakeDeps() {
        dependencies = new Dictionary<string, Dependency>();
    }

}
