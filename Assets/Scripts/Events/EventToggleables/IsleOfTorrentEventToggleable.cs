using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IsleOfTorrentEventToggleable : MonoBehaviour, IEventToggleable
{
    [SerializeField] private GameObject pirates;
    [SerializeField] private GameObject aristocrats;
    
    public void ToggleEventEffects(int sceneIndex)
    {
        if (sceneIndex == 2)
        {
            pirates.SetActive(true);
            aristocrats.SetActive(false);
        }
    }
}
