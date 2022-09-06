using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static CinemachineVirtualCamera VirtualCamera;
    public static PlayerController[] Dogs = new PlayerController[3];
    public PlayerController Lou;
    public PlayerController Lucy;
    public PlayerController Skye;
    public PlayerInput PlayerInput;
    public InputAction _louSwitch;
    public InputAction _skyeSwitch;

    public static PlayerController _currentDog;
    private static int _dogIndex;
    private void Start()
    {
        VirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();

        _louSwitch = PlayerInput.actions["LouSwitch"];
        _louSwitch.performed += context => LouSwitch(context);

        _skyeSwitch = PlayerInput.actions["SkyeSwitch"];
        _skyeSwitch.performed += context => SkyeSwitch(context);
        
        Dogs[0] = Lou;
        Dogs[1] = Skye;
        Dogs[2] = Lucy;
        
        _dogIndex = 0;

        _currentDog = Dogs[_dogIndex];
        _currentDog.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void LouSwitch(InputAction.CallbackContext context)
    {
        print("changing to Lou");
        VirtualCamera.Follow = Lou.transform;
        _currentDog = Lou;
    }

    private void SkyeSwitch(InputAction.CallbackContext context)
    {
        print("changing to Skye");
        VirtualCamera.Follow = Skye.transform;
        _currentDog = Skye;
    }

    public static void SwitchDog()
    {

        //myVal = (myVal++) % (maxValue+1);
        _dogIndex++;
        if (_dogIndex > 2) {
            _dogIndex = 0;
        }
        _currentDog.gameObject.layer = LayerMask.NameToLayer("Ground");
        
        _currentDog = Dogs[_dogIndex];

        _currentDog.gameObject.layer = LayerMask.NameToLayer("Default");

        print($"current dog is { _currentDog }! ");
        Inputs.switching = false;
        VirtualCamera.Follow = _currentDog.transform;
    }
}
