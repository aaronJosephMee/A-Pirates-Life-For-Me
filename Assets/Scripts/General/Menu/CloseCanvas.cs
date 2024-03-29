using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.settingsMenu.activeSelf)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            canvas.gameObject.SetActive(true);
        }
    }
}
