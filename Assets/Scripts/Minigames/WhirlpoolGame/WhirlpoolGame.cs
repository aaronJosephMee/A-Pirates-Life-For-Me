using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class WhirlpoolGame : MonoBehaviour
{
    public Slider playerSlider;
    public Transform whirlpoolCenter;
    public GameObject ship;
    public float rotationSpeed;
    public float translationSpeed;
    public float distanceFromCenter;
    public float whirlpoolSize;

    public AudioSource backgroundMusic;
    
    public GameObject winningText;
    public GameObject losingText;
    public GameObject howToPlayPanel;


    
    private bool isBeingPulled;

    private void Start()
    {
        // Upper limit for slider (1.5 times the size of the whirlpool)
        whirlpoolSize = (float) 1.5 * whirlpoolCenter.gameObject.GetComponent<Renderer>().bounds.size.magnitude;
        winningText.gameObject.SetActive(false);
        losingText.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(true);
    }

    void Update()
    {
        if (isBeingPulled)
        {
            RotateAroundWhirlpool(ship);
            TranslateTowardsCenter(ship);
            TranslateTowardsPerimeter();
            RotateAroundWhirlpool(whirlpoolCenter.gameObject);
        }

        // Check for user input (adjust this based on your existing button-mashing logic)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isBeingPulled = true;
            howToPlayPanel.gameObject.SetActive(false);
        }
        distanceFromCenter = Vector3.Distance(ship.gameObject.transform.position, whirlpoolCenter.position);
        playerSlider.value = distanceFromCenter / (whirlpoolSize / 2);

        if (isBeingPulled)
        {
            if (playerSlider.value >= 1)
            {
                EndGame(true);

            } else if (playerSlider.value <= 0)
            {
                EndGame(false);
            }
        }
    }

    private void EndGame(bool won)
    {
        isBeingPulled = false;
        if (won)
        {
            winningText.gameObject.SetActive(true);
        }
        else
        {
            losingText.gameObject.SetActive(true);
        }
        Time.timeScale = 0.0f;
        backgroundMusic.Stop();
        
        StartCoroutine(EndGameRoutine());
    }
    
    private IEnumerator EndGameRoutine()
    {
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1.0f;
        OverworldMapManager.Instance.TransitionBackToMap();
    }

    void RotateAroundWhirlpool(GameObject obj)
    {
        // Rotate the object around the whirlpool
        float rotationMultiplier = 7.5f * (1.0f - playerSlider.value);
        obj.transform.RotateAround(whirlpoolCenter.position, Vector3.up, rotationMultiplier * rotationSpeed * Time.deltaTime);
    }

    void TranslateTowardsCenter(GameObject obj)
    {
        // Translate the object towards the center of the whirlpool
        float step = translationSpeed * Time.deltaTime;
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, whirlpoolCenter.position, step);
        
        // Music and difficulty adjustments
        
        if (playerSlider.value <= 0.10)
        {
            backgroundMusic.pitch += 0.2f * Time.deltaTime;
        }
        
        else if (playerSlider.value <= 0.25)
        {
            backgroundMusic.pitch += 0.1f * Time.deltaTime;
            
            translationSpeed = 5;
        }
        
        else if (playerSlider.value <= 0.75)
        {
            backgroundMusic.pitch += 0.05f * Time.deltaTime;
        }
        
        else if (playerSlider.value <= 0.75)
        {
            backgroundMusic.pitch = 1f;
            
            translationSpeed = 10;
        }
        else
        {
            translationSpeed = 20;

            if (backgroundMusic.pitch > 0.5)
            {
                backgroundMusic.pitch -= 0.2f * Time.deltaTime;
            }
        }
    }

    void TranslateTowardsPerimeter()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.deltaTime != 0f)
        {
            // Controls how fast you are moving away from the whirlpool when you press Z
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, whirlpoolCenter.position, -5.0f);

            if (backgroundMusic.pitch > 0.5)
            {
                backgroundMusic.pitch -= 5f * Time.deltaTime;
            }
        }
    }
}

