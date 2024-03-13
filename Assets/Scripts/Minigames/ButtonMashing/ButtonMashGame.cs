using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMashGame : MonoBehaviour
{
    public enum Difficulty
    {
        EASY, 
        MEDIUM,
        CHALLENGING
    }
    
    public Slider playerSlider;
    public TextMeshProUGUI winningText;
    public TextMeshProUGUI losingText;

    private float playerProgress;
    private float cpuProgress;
    private bool gameEnded;

    private Difficulty difficulty;
    
    private const float PlayerMashSpeed = 5f;  
    private float CPUMashSpeed = 9f;      
    private float CPUMashSpeedIncrease = 9f;  
    private float CPUMashSpeedDecrease = 2f;  
    
    
    private void Start()
    {
        // set winning/losing text to be inactive
        winningText.gameObject.SetActive(false);
        losingText.gameObject.SetActive(false);
        
        // player's starting position, change if necessary
        playerProgress = 30f;
        playerSlider.value = playerProgress;
        
        // adjust game difficulty here
        difficulty = Difficulty.EASY;
    }
    
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
        
        ChallengeLevel(difficulty);

        UpdateUI();

        CheckGameResult();
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
            winningText.gameObject.SetActive(true);
            Debug.Log("You win!");
            
        }
        else if (playerSlider.value <= 0)
        {
            // CPU wins!
            losingText.gameObject.SetActive(true);
            Debug.Log("You lose!");
        }

        if (playerSlider.value >= 100 || playerSlider.value <= 0)
        {
            gameEnded = true;
            //StartCoroutine(EndGameRoutine());
        }
    }
    
    private IEnumerator EndGameRoutine()
    {
        yield return new WaitForSeconds(2);
        OverworldMapManager.Instance.TransitionBackToMap();
        // GameManager.instance.LoadScene("Rusty's Retreat", true);
    }


    void ChallengeLevel(Difficulty difficulty)
    {
        if (difficulty == Difficulty.EASY)
        {
            Debug.Log("easy difficulty activated");
            
            // Adjust CPU mash speed based on player progress
            if (playerSlider.value > 50)
            {
                CPUMashSpeed = 24f;
                Debug.Log("increasing difficulty");
            }
            else if (playerSlider.value < 25)
            {
                CPUMashSpeed -= CPUMashSpeedDecrease * Time.deltaTime;
                // reset CPUMashSpeed and CPUMashSpeedIncrease
                CPUMashSpeed = 9f;
                CPUMashSpeedIncrease = 6f;
                Debug.Log("decreasing difficulty");
            }
        }

        if (difficulty == Difficulty.MEDIUM)
        {
            Debug.Log("medium difficulty activated");
            
            // Adjust CPU mash speed based on player progress
            if (playerSlider.value > 50)
            {
                CPUMashSpeed = 24f;
                Debug.Log("increasing difficulty");
            }
            else if (playerSlider.value > 75)
            {
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
        }

        if (difficulty == Difficulty.CHALLENGING)
        {
            Debug.Log("challenging difficulty activated");
            
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
        }
    }
}
