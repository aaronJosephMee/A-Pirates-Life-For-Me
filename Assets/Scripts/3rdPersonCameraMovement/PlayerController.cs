using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private const float Gravity = -9.81f;
    private const float RotationSpeed = 500f;
    private float _downwardsVelocity;
    private Vector2 _input;
    private Vector3 _direction;
    private CharacterController _characterController;
    private Animator _playerModelAnimator;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject cameraPosition;
    [SerializeField] private float speed;
    private static readonly int RunningAnimationFlag = Animator.StringToHash("Running");

    private void Awake()
    {
        _playerModelAnimator = playerModel.GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        SetAnimationFlags();
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void SetAnimationFlags()
    {
        _playerModelAnimator.SetBool(RunningAnimationFlag, _input.sqrMagnitude != 0);
    }

    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0)
        {
            return;
        }
        _direction = Quaternion.Euler(0.0f, cameraPosition.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
        Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
        playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, targetRotation,
            RotationSpeed * Time.deltaTime);
    }
    
    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _direction.y < 0.0f)
        {
            _downwardsVelocity = -1.0f;
        }
        else
        {
            _downwardsVelocity += Gravity * Time.deltaTime;
        }

        _direction.y = _downwardsVelocity;
    }
    
    private void ApplyMovement()
    {
        _characterController.Move(_direction * (speed * Time.deltaTime));
    }
    
    public void GetMovementInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }
}
