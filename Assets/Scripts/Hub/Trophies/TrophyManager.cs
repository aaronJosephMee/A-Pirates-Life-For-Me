using System;
using UnityEngine;

namespace DefaultNamespace.Hub
{
    public class TrophyManager : MonoBehaviour
    {
        public void Update()
        {
            Transform one = transform.Find("2");
            one.gameObject.SetActive(true);
        }
    }
    
}