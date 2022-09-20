using Cinemachine;
using TP2.Systems.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerController CurrentDog { get; private set; }

    public static CinemachineVirtualCamera VirtualCamera;
    public static PlayerController[] Dogs = new PlayerController[3];
    public PlayerController Lou;
    public PlayerController Lucy;
    public PlayerController Skye;
    public PlayerInput PlayerInput;
    public InputAction _louSwitch;
    public InputAction _skyeSwitch;


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

        CurrentDog = Dogs[_dogIndex];
        CurrentDog.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void SwitchDog()
    {

        //myVal = (myVal++) % (maxValue+1);
        _dogIndex++;
        if (_dogIndex > 2)
        {
            _dogIndex = 0;
        }
        CurrentDog.gameObject.layer = LayerMask.NameToLayer("Ground");

        CurrentDog = Dogs[_dogIndex];

        CurrentDog.gameObject.layer = LayerMask.NameToLayer("Default");

        print($"current dog is {CurrentDog}! ");
        Inputs.switching = false;
        VirtualCamera.Follow = CurrentDog.transform;
    }

    private void LouSwitch(InputAction.CallbackContext context)
    {
        print("changing to Lou");
        VirtualCamera.Follow = Lou.transform;
        CurrentDog = Lou;
    }

    private void SkyeSwitch(InputAction.CallbackContext context)
    {
        print("changing to Skye");
        VirtualCamera.Follow = Skye.transform;
        CurrentDog = Skye;
    }


}
