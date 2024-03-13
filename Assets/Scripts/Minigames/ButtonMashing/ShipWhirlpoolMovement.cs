using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipWhirlpoolMovement : MonoBehaviour
{
    public Slider playerSlider;
    public Transform whirlpoolCenter;
    public GameObject ship;
    public float rotationSpeed = 10f;
    public float translationSpeed = 5f;
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
            RotateAroundWhirlpool(whirlpoolCenter.gameObject);
        }

        // Check for user input (adjust this based on your existing button-mashing logic)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isBeingPulled = true;
            
        }
        distanceFromCenter = Vector3.Distance(ship.gameObject.transform.position, whirlpoolCenter.position);
        playerSlider.value = distanceFromCenter / whirlpoolSize;

        if (playerSlider.value >= 1)
        {
            winningText.gameObject.SetActive(true);
            //TODO STOP IT, GET SOME HELP
        } else if (playerSlider.value <= 0)
        {
            losingText.gameObject.SetActive(true);
        }
    }

    void RotateAroundWhirlpool(GameObject obj)
    {
        // Rotate the object around the whirlpool
        obj.transform.RotateAround(whirlpoolCenter.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void TranslateTowardsCenter(GameObject obj)
    {
        // Translate the object towards the center of the whirlpool
        float step = translationSpeed * Time.deltaTime;
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, whirlpoolCenter.position, step);
    }
}

