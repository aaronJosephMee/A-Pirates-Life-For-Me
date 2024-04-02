using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    public GameObject hubTut;
    public GameObject p;
    public GameObject page1;
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        p.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        page1.SetActive(true);
        
    }

    private void Update()
    {
        
        if (hubTut == null)
        {
           p.SetActive(true);
        }
       
    }

    
}
