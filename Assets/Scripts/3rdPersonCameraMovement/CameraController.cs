using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float horizontalSensitivity;
    [SerializeField] private float verticalSensitivity;
    
    private Vector2 _cameraRotation = Vector2.zero;
    private Vector2 _mouseInput;
    
    // Start is called before the first frame update
    private void Start()
    {
        HideCursor();
    }
    
    private void Update()
    {
        CalculateCameraPosition();
    }
    
    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(-_cameraRotation.y, _cameraRotation.x, 0);
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CalculateCameraPosition()
    {
        _cameraRotation.x += _mouseInput.x * horizontalSensitivity * Time.deltaTime;
        _cameraRotation.y += _mouseInput.y * verticalSensitivity * Time.deltaTime;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y,-85.0f, 85.0f);
    }

    public void GetMouseInput(InputAction.CallbackContext context)
    {
        _mouseInput = context.ReadValue<Vector2>();
    }
}
