using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverworldCameraController : MonoBehaviour
{
    private Vector2 _input = Vector2.zero;

    private float _cameraSpeed = 1000.0f;
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
    }

    private void LateUpdate()
    {
        if (_input.x != 0)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += _input.x * _cameraSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, -250.0f, 250.0f);
            transform.position = newPosition;
        }
    }
}
