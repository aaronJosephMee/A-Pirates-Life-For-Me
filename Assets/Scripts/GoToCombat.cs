using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;

public class GoToCombat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBattle()
    {
        GameManager.instance.SetCombatIndex(11);
        GameManager.instance.LoadScene(SceneName.CombatNight);
    }
}
