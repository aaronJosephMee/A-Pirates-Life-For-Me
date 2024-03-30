using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverworldCameraController : MonoBehaviour
{
    private Vector2 _input = Vector2.zero;

    [SerializeField] private float cameraSpeed;

    [SerializeField] private float minPosition;

    [SerializeField] private float maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GetMovementInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        print(gameObject.name);
    }

    private void LateUpdate()
    {
        print(gameObject.name);
        if (_input.x != 0)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += _input.x * cameraSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, minPosition, maxPosition);
            transform.position = newPosition;
        }
    }
}
