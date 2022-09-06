using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction _jumpAction;


    public static InputAction _goToNextDogAction;
    private static Vector2 InputAxis;
    public static bool jumping = false;
    public static bool switching = false;

    // Start is called before the first frame update
    private void Awake() 
    {
        _jumpAction = playerInput.actions["Jump"];
        _jumpAction.started += context => OnJumpPerformed(context);
        _jumpAction.canceled += context => OnJumpCanceled(context);

        _goToNextDogAction = playerInput.actions["GoToNextDog"];
        _goToNextDogAction.performed += context => OnGoToNextDogPerformed(context);
        _goToNextDogAction.canceled += context => OnGoToNextDogCanceled(context);
    }
    private void OnDisable() {
        _jumpAction.started -= OnJumpPerformed;
        _jumpAction.canceled -= OnJumpCanceled;
        _goToNextDogAction.performed -= OnGoToNextDogPerformed;
        _goToNextDogAction.canceled -= OnGoToNextDogCanceled;
    }
    private void OnGoToNextDogPerformed(InputAction.CallbackContext context)
    {
        switching = true;
    }

    private void OnGoToNextDogCanceled(InputAction.CallbackContext context)
    {
        switching = false;
    }

    public void OnMovement(InputValue val)
    {
        InputAxis = val.Get<Vector2>();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumping = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {   
        jumping = false;
    }

    public static bool FacingRight() 
    {
        return InputAxis.x == 1;
    }
    public static bool FacingLeft() 
    {
        return InputAxis.x == -1;
    }
    public static bool IsMoving() 
    {
        return InputAxis.x != 0;
    }

    public static bool IsCrouching() 
    {
        return InputAxis.y == -1;
    }

    public static bool IsJumping() 
    {
        return jumping;
    }

    public static float GetHorizontalInput() 
    {
        return InputAxis.x;
    }

    internal static bool IsMovingUp()
    {
        return InputAxis.y > 0;
    }
    
}
