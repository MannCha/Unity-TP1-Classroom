using System;
using System.Collections;
using System.Collections.Generic;
using TP2.Systems.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : Singleton<Inputs>
{
    public PlayerInput playerInput;
    public InputAction GoToNextDogAction;
    private InputAction JumpAction;
    private Vector2 InputAxis;
    public bool Jumping { get; private set; }
    public bool Switching = false;

    private void Start() 
    {
        Debug.Log(Instance);
        JumpAction = playerInput.actions["Jump"];
        JumpAction.started += context => OnJumpPerformed(context);
        JumpAction.canceled += context => OnJumpCanceled(context);

        GoToNextDogAction = playerInput.actions["GoToNextDog"];
        GoToNextDogAction.performed += context => OnGoToNextDogPerformed(context);
        GoToNextDogAction.canceled += context => OnGoToNextDogCanceled(context);
    }

    private void OnDisable() 
    {
        JumpAction.started -= OnJumpPerformed;
        JumpAction.canceled -= OnJumpCanceled;
        GoToNextDogAction.performed -= OnGoToNextDogPerformed;
        GoToNextDogAction.canceled -= OnGoToNextDogCanceled;
    }
    private void OnGoToNextDogPerformed(InputAction.CallbackContext context)
    {
        Switching = true;
    }

    private void OnGoToNextDogCanceled(InputAction.CallbackContext context)
    {
        Switching = false;
    }

    public void OnMovement(InputValue val)
    {
        InputAxis = val.Get<Vector2>();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Jumping = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {   
        Jumping = false;
    }

    public bool FacingRight() 
    {
        return InputAxis.x == 1;
    }
    public bool FacingLeft() 
    {
        return InputAxis.x == -1;
    }
    public bool IsMoving() 
    {
        return InputAxis.x != 0;
    }

    public bool IsCrouching() 
    {
        return InputAxis.y == -1;
    }

    public bool IsJumping() 
    {
        return Jumping;
    }

    public float GetHorizontalInput() 
    {
        return InputAxis.x;
    }

    internal bool IsMovingUp()
    {
        return InputAxis.y > 0;
    }
    
}
