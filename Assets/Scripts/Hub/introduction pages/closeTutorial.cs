using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class closeTutorial : MonoBehaviour
{

   public GameObject p; 
   private void Start()
   {
      
      this.GetComponent<Button>().onClick.AddListener(delegate{Destroy(this.transform.parent.gameObject);});
      
   }
   
}
