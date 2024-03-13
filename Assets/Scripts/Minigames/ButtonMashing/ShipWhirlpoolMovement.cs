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

    
    private bool isBeingPulled;

    private void Start()
    {
        // Upper limit for slider (1.5 times the size of the whirlpool)
        whirlpoolSize = (float) 1.5 * whirlpoolCenter.gameObject.GetComponent<Renderer>().bounds.size.magnitude;
        winningText.gameObject.SetActive(false);
        losingText.gameObject.SetActive(false);
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
            
        }
        distanceFromCenter = Vector3.Distance(ship.gameObject.transform.position, whirlpoolCenter.position);
        playerSlider.value = distanceFromCenter / (whirlpoolSize / 2);

        if (playerSlider.value >= 1)
        {
            winningText.gameObject.SetActive(true);

        } else if (playerSlider.value <= 0)
        {
            losingText.gameObject.SetActive(true);
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
    }

    void TranslateTowardsPerimeter()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Controls how fast you are moving away from the whirlpool when you press Z
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, whirlpoolCenter.position, -3.0f);
        }
    }
}

