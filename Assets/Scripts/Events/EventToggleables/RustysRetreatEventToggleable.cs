using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RustysRetreatEventToggleable : MonoBehaviour, IEventToggleable
{
    [SerializeField] private GameObject hostileRogue;
    [SerializeField] private GameObject neutralRogue;
    private List<GameObject> cameras = new List<GameObject>();
    private void Start()
    {
        foreach (Transform child in transform)
        {
            cameras.Add(child.gameObject);
        }
        print(cameras.Count);
    }

    public void ToggleEventEffects(int sceneIndex)
    {
        if (sceneIndex == 2)
        {
            hostileRogue.SetActive(false);
            neutralRogue.SetActive(true);
        }
    }
    

    public void ChangeVisuals(int index)
    {
        if (index == 3)
        {
            foreach (GameObject camera in cameras)
            {
                if (camera.activeSelf)
                {
                    camera.SetActive(false);
                    break;
                }
            }
            cameras[index-1].SetActive(true);
        }

        if (index == 5)
        {
            hostileRogue.SetActive(true);
            neutralRogue.SetActive(false);
        }

    }
}
