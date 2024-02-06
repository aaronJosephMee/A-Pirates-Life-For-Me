using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float _gravity = -9.81f;
    private float _velocity;
    private Vector2 _input;
    private Vector3 _direction;
    private CharacterController _characterController;
    [SerializeField] private GameObject _playerModel;
    private Animator _playerModelAnimator;
    [SerializeField] private GameObject _cameraPosition;

    [SerializeField] private float _speed;
    private float _currentVelocity;
    private float rotationSpeed = 500f;
    private static readonly int Running = Animator.StringToHash("Running");

    private void Awake()
    {
        _playerModelAnimator = _playerModel.GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyAnimations();
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyAnimations()
    {
        _playerModelAnimator.SetBool(Running, _input.sqrMagnitude != 0);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * Time.deltaTime;
        }

        _direction.y = _velocity;
    }
    
    private void ApplyMovement()
    {
        _characterController.Move(_direction * (_speed * Time.deltaTime));
    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;
        _direction = Quaternion.Euler(0.0f, _cameraPosition.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
        var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
        _playerModel.transform.rotation = Quaternion.RotateTowards(_playerModel.transform.rotation, targetRotation,
            rotationSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }
}
