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
    
    private bool isEasy = false;
    private bool isMedium = false;
    private bool isChallenging = false;
    
    private float PlayerMashSpeed = 5f;  
    private float CPUMashSpeed = 9f;      
    private float CPUMashSpeedIncrease = 9f;  
    private float CPUMashSpeedDecrease = 2f;  


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
        
        // Adjust CPU mash speed based on player progress
        if (playerSlider.value > 50)
        {
            CPUMashSpeedIncrease = 12f;
            CPUMashSpeed += CPUMashSpeedIncrease * Time.deltaTime;
            Debug.Log("increasing difficulty");
        }
        else if (playerSlider.value > 75)
        {
            CPUMashSpeed = 9f;
            CPUMashSpeed += CPUMashSpeedIncrease * Time.deltaTime;
        }
        else if (playerSlider.value < 25)
        {
            CPUMashSpeed -= CPUMashSpeedDecrease * Time.deltaTime;
            // reset CPUMashSpeed and CPUMashSpeedIncrease
            CPUMashSpeed = 9f;
            CPUMashSpeedIncrease = 6f;
            Debug.Log("decreasing difficulty");
        }

        UpdateUI();

        CheckGameResult();
    }

    private void Start()
    {
        playerProgress = 30f;
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

    void ChallengeLevel(bool difficulty)
    {
        if (isEasy)
        {
            
        }
    }
}
