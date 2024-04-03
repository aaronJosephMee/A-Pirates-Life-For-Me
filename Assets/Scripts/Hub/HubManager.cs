using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    public GameObject hubTut;
    public GameObject p;
    public GameObject page1;
    private static bool seen;  
    private void Awake()
    {
        
    }

    private void Start()
    {
        if (seen == false)
        {
            p.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            page1.SetActive(true);
        }
       

    }

    private void Update()
    {
        
        
        if (hubTut == null)
        {
            p.SetActive(true);
            seen = true; 
        }

        if (seen)
        {
            Destroy(hubTut);
        }
       
    }

    
}
