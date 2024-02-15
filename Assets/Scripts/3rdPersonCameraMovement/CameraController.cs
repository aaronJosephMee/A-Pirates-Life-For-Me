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
    private bool _receiveMouseInput;
    
    // Start is called before the first frame update
    private void Start()
    {
        HideCursor();
    }
    
    private void Update()
    {
        if (_receiveMouseInput)
        {
            CalculateCameraPosition();
        }
    }
    
    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(-_cameraRotation.y, _cameraRotation.x, 0);
    }
    
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _receiveMouseInput = false;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _receiveMouseInput = true;
    }

    private void CalculateCameraPosition()
    {
        _cameraRotation.x += _mouseInput.x * horizontalSensitivity * Time.deltaTime;
        _cameraRotation.y += _mouseInput.y * verticalSensitivity * Time.deltaTime;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y,-85.0f, 50.0f);
    }

    public void GetMouseInput(InputAction.CallbackContext context)
    {
        _mouseInput = context.ReadValue<Vector2>();
    }
}
