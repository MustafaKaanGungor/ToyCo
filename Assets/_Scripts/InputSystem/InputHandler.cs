using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private static bool _isInitialized = false;
    private void OnEnable()
    {
        _inputActions.UI.Enable();
        _inputActions.Player.Enable();
        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMovePerformed;

    }

    private void OnDisable()
    {
        _inputActions.UI.Disable();
        _inputActions.Player.Disable();
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMovePerformed;


    }
    private void Awake()
    {
        if (_isInitialized)
        {
            Destroy(this);
            return;
        }
        _inputActions = new InputSystem_Actions();
        _isInitialized = true;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnTogglePlayerInput(bool obj)
    {
        if (obj)
        {
            _inputActions.Player.Enable();
        }
        else
        {
            _inputActions.Player.Disable();
        }
    }
    private void OnToggleUIInput()
    {
        bool isUIActive = _inputActions.UI.enabled;
        if (isUIActive)
        {
            _inputActions.UI.Disable();
        }
        else
        {
            _inputActions.UI.Enable();

        }
    }
    private void OnToggleUISubmit()
    {
        bool isUIActive = _inputActions.UI.Submit.enabled;
        if (isUIActive)
        {
            _inputActions.UI.Submit.Disable();
        }
        else
        {
            _inputActions.UI.Submit.Enable();
        }
    }

   


    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        GameEvents.TriggerMoveInput(context.ReadValue<Vector2>());
    }

}