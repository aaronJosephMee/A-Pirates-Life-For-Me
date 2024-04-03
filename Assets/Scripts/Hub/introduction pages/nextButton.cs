using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class shipHubTut : MonoBehaviour
{
   public GameObject page1;
   public GameObject page2;
   public GameObject button; 
   private void Start()
   {
      GetComponent<Button>().onClick.AddListener(next);
   }


   private void next()
   {
      button.SetActive(false);
      page1.SetActive(false);
      page2.SetActive(true);
   }
}

