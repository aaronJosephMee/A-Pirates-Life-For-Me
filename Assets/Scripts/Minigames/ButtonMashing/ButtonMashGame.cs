using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMashGame : MonoBehaviour
{
    public Slider playerSlider;

    private float playerProgress;
    private float cpuProgress;
    private bool gameEnded;

    
    private float PlayerMashSpeed = 5f;  
    private float CPUMashSpeed = 6f;      

    void Update()
    {
        if (gameEnded)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            Debug.Log("im pressed down");
            playerProgress += PlayerMashSpeed;
        }

        // Simulate CPU progress (you can adjust this logic)
        cpuProgress += Time.deltaTime * CPUMashSpeed;

        UpdateUI();

        CheckGameResult();
    }

    private void Start()
    {
        playerProgress = 50f;
        playerSlider.value = playerProgress;
    }

    void UpdateUI()
    {
        playerSlider.value = playerProgress - cpuProgress;
    }

    void CheckGameResult()
    {
        if (playerSlider.value >= 100)
        {
            // Player wins!
            Debug.Log("You win!");
            
        }
        else if (playerSlider.value <= 0)
        {
            // CPU wins!
            Debug.Log("You lose!");
        }

        if (playerSlider.value >= 100 || playerSlider.value <= 0)
        {
            gameEnded = true;
        }
    }
}
