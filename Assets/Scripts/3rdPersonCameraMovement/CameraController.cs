using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    private Vector2 _cameraRotation = Vector2.zero;

    private float _horizontalSensitivity = 20.0f;
    private float _verticalSensitivity = 20.0f;

    private Vector2 _input;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RotateCamera(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();

    }
    // Update is called once per frame
    void Update()
    {
        _cameraRotation.x += _input.x * _horizontalSensitivity * Time.deltaTime;
        _cameraRotation.y += _input.y * _verticalSensitivity * Time.deltaTime;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y,-85.0f, 85.0f);
        transform.localRotation = Quaternion.Euler(-_cameraRotation.y, _cameraRotation.x, 0);
    }

    private void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(-_cameraRotation.y, _cameraRotation.x, 0);
    }
}
