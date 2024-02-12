using System;
using System.Collections.Generic;
using System.IO;
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

    //Adds a flag with value = startVal
    public void AddFlag(String name, int startVal){
        int newFlag;
        newFlag = startVal;
        flags.Add(name, newFlag);
    }
    //Returns the value of the flag
    public int CheckFlag(String flag){
        int val;
        if (!flags.TryGetValue(flag, out val)){
            Debug.Log("Requested flag '" + flag + "' doesn't exist");
            return -1;
        }
        return val;
    }
    //Creates a dependency from a gameObject to a flag on a specific value. On that value it calls the function given in whatDo
    public void CreateDependency(String flag, GameObject gameObject, int onVal, Func<GameObject, int> whatDo){
        int state;
        if (!flags.TryGetValue(flag, out state)){
            Debug.Log("Requested flag '" + flag +"' doesn't exist");
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
    //Sets the value of a flag to newVal and calls any dependencies on that value
    public void SetFlag(String flag, int newVal){
        int val;
        if (!flags.TryGetValue(flag, out val)){
            Debug.Log("Requested flag '" + flag + "' doesn't exist");
            return;
        }
        val = newVal;
        flags[flag] = val;
        Dependency dep;
        Func<GameObject, int> toDo;
        if (dependencies.TryGetValue(flag, out dep)){
            foreach (KeyValuePair<GameObject, Dictionary<int, Func<GameObject, int>>> entry in dep.deps){
                if (entry.Value.TryGetValue(newVal, out toDo) && entry.Key != null){   
                    toDo(entry.Key);
                }
            }
        }
        
    }
    //Remakes the dependencies. (Needed because the gameObjects in the dependencies stop existing on scene change)
    public void RemakeDeps() {
        dependencies = new Dictionary<string, Dependency>();
    }

    public void SaveState(){
        string data = "";
        data += SceneManager.GetActiveScene().name + "\n";
        foreach (KeyValuePair<string, int> entry in flags){
            data += entry.Key + " " + entry.Value + "\n";
        }
        Debug.Log(Application.dataPath + "/Saves/savefile");
        File.WriteAllText(Application.dataPath + "/Saves/savefile", data);
    }

}
