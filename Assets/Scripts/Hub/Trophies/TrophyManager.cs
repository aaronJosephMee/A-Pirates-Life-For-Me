using System;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

namespace DefaultNamespace.Hub
{
    public class TrophyManager : MonoBehaviour
    {
        public Boolean trophy1Flag = false;
        public Boolean trophy2Flag = false;
        public Boolean trophy3Flag = false;
        public Boolean trophy4Flag = false; 
        public Boolean trophy5Flag = false;
        public Boolean trophy6Flag = false;
        public Boolean trophy7Flag = false;
        public Boolean trophy8Flag = false;
        public Boolean trophy9Flag = false;
        
        public void Start()
        {
            
        }

        public void Update()
        {
            Transform trophy1 = transform.Find("1");
            Transform trophy2 = transform.Find("2");
            Transform trophy3 = transform.Find("3");
            Transform trophy4 = transform.Find("4");
            Transform trophy5 = transform.Find("5");
            Transform trophy6 = transform.Find("6");
            Transform trophy7 = transform.Find("7");
            Transform trophy8 = transform.Find("8");
            Transform trophy9 = transform.Find("9");
            
            if (trophy1Flag)
            {
                trophy1.gameObject.SetActive(true);
            }
            
            if (trophy2Flag)
            {
                trophy2.gameObject.SetActive(true);
            }
            
            if (trophy3Flag)
            {
                trophy3.gameObject.SetActive(true);
            }
            
            if (trophy4Flag)
            {
                trophy4.gameObject.SetActive(true);
            }
            
            if (trophy5Flag)
            {
                trophy5.gameObject.SetActive(true);
            }
            
            if (trophy6Flag)
            {
                trophy6.gameObject.SetActive(true);
            }
            
            if (trophy7Flag)
            {
                trophy7.gameObject.SetActive(true);
            }
            
            if (trophy8Flag)
            {
                trophy8.gameObject.SetActive(true);
            }
            
            if (trophy9Flag)
            {
                trophy9.gameObject.SetActive(true);
            }
            
            
        }
    }
    
}