using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScreen : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    [SerializeField] private TextMeshProUGUI descriptionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignContinueLocation(SceneName sceneName)
    {
        continueButton.onClick.AddListener(() => GameManager.instance.LoadScene(sceneName));
    }

    public void SetText(String text)
    {
        descriptionText.text = text;
    }
}