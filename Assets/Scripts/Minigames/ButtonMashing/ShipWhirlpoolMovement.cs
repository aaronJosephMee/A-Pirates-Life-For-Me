using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipWhirlpoolMovement : MonoBehaviour
{
    public Slider playerSlider;
    public Transform whirlpoolCenter;
    public GameObject ship;
    public float rotationSpeed;
    public float translationSpeed;
    public float distanceFromCenter;
    public float whirlpoolSize;
    
    public TextMeshProUGUI winningText;
    public TextMeshProUGUI losingText;
    public TextMeshProUGUI howToPlayText;


    
    private bool isBeingPulled;

    private void Start()
    {
        // Upper limit for slider (1.5 times the size of the whirlpool)
        whirlpoolSize = (float) 1.5 * whirlpoolCenter.gameObject.GetComponent<Renderer>().bounds.size.magnitude;
        winningText.gameObject.SetActive(false);
        losingText.gameObject.SetActive(false);
        howToPlayText.gameObject.SetActive(true);
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
            howToPlayText.gameObject.SetActive(false);
        }
        distanceFromCenter = Vector3.Distance(ship.gameObject.transform.position, whirlpoolCenter.position);
        playerSlider.value = distanceFromCenter / (whirlpoolSize / 2);

        if (playerSlider.value >= 1)
        {
            winningText.gameObject.SetActive(true);
            Time.timeScale = 0f;

        } else if (playerSlider.value <= 0)
        {
            losingText.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
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
        
        if (playerSlider.value <= 0.25)
        {
            translationSpeed = 5;
        }
        else if (playerSlider.value <= 0.75)
        {
            translationSpeed = 10;
        }
        else
        {
            translationSpeed = 20;
        }
    }

    void TranslateTowardsPerimeter()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.deltaTime != 0f)
        {
            // Controls how fast you are moving away from the whirlpool when you press Z
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, whirlpoolCenter.position, -5.0f);
        }
    }
}

