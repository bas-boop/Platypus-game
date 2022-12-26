using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class InputParcer : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;

    [Header("Scripts")] 
    [SerializeField] private PlayerBasicMovement playerMovement;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
        
        _playerControlsActions["Roll"].performed += Roll;
    }

    private void FixedUpdate()
    {
        var moveInput = _playerControlsActions["Move"].ReadValue<Vector2>();
        playerMovement.SetMoveDirection(moveInput);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        _playerControlsActions["Roll"].performed -= Roll;
    }

    private void Roll(InputAction.CallbackContext context) => playerMovement.ActivateRoll();
}
