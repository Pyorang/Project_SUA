using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    private bool isKeyBoard = UserDataManager.Instance.GetUserData<UserSettingData>().IsKeyBoard;

    [Header("Controllers")]
    [Space]

    [SerializeField] private WheelController wheelController;
    [SerializeField] private LightController lightController;

    #region Gears
    private Gear D, N, R, P;
    private Gear currentGear;
    #endregion
    #region InputSystems
    KeyBoardInputActions action;
    InputAction frontMoveKeyBoard;
    InputAction frontMoveSteeringWheel;
    InputAction leftRightMoveKeyBoard;
    InputAction leftRightSteeringWheel;
    InputAction breakKeyBoard;
    InputAction breakSteeringWheel;
    InputAction lightControlKeyBoard;
    InputAction lightControlSteeringWheel;
    InputAction sideBreakKeyBoard;
    InputAction sideBreakSteeringWheel;
    InputAction GearChangeKeyBoard;
    InputAction GearChangeSteeringWheel;
    #endregion

    private void Awake()
    {
        AllocateInputActions();
        InitGears();
    }

    private void OnEnable()
    {
        EnableAllActions(isKeyBoard);
    }

    private void OnDisable()
    {
        DisableAllActions(isKeyBoard);
    }

    private void FixedUpdate()
    {
        currentGear.Drive();
    }

    public WheelController GetWheelController() { return wheelController; }

    void AllocateInputActions()
    {
        action = new KeyBoardInputActions();
        frontMoveKeyBoard = action.Car.FrontMoveKeyBoard;
        frontMoveSteeringWheel = action.Car.FrontMoveSteeringWheel;
        leftRightMoveKeyBoard = action.Car.LeftRightMoveKeyBoard;
        leftRightSteeringWheel = action.Car.LeftRightMoveSteeringWheel;
        breakKeyBoard = action.Car.BreakKeyBoard;
        breakSteeringWheel = action.Car.BreakSteeringWheel;
        lightControlKeyBoard = action.Car.LightControlKeyBoard;
        lightControlSteeringWheel = action.Car.LightControlSteeringWheel;
        sideBreakKeyBoard = action.Car.SideBreakKeyBoard;
        sideBreakSteeringWheel = action.Car.SideBreakSteeringWheel;
        GearChangeKeyBoard = action.Car.GearChangeKeyBoard;
        GearChangeSteeringWheel = action.Car.GearChangeSteeringWheel;
    }

    void InitGears()
    {
        D = new DriveGear(this);
        N = new NeutralGear(this);
        R = new ReverseGear(this);
        P = new ParkingGear(this);
        currentGear = P;
    }

    void EnableAllActions(bool isKeyBoard)
    {
        if(isKeyBoard)
        {
            frontMoveKeyBoard.Enable();
            leftRightMoveKeyBoard.Enable();
            breakKeyBoard.Enable();
            lightControlKeyBoard.Enable();
            lightControlKeyBoard.performed += GetLightControl;
            sideBreakKeyBoard.Enable();
            sideBreakKeyBoard.performed += GetSideBreakControl;
            GearChangeKeyBoard.Enable();
            GearChangeKeyBoard.performed += GetGearChangeControl;
        }
        else
        {
            frontMoveSteeringWheel.Enable();
            leftRightSteeringWheel.Enable();
            breakSteeringWheel.Enable();
            lightControlSteeringWheel.Enable();
            lightControlSteeringWheel.performed += GetLightControl;
            sideBreakSteeringWheel.Enable();
            sideBreakSteeringWheel.performed += GetSideBreakControl;
            GearChangeSteeringWheel.Enable();
            GearChangeSteeringWheel.performed += GetGearChangeControl;
        }
    }

    void DisableAllActions(bool isKeyBoard)
    {
        if (isKeyBoard)
        {
            frontMoveKeyBoard.Disable();
            leftRightMoveKeyBoard.Disable();
            breakKeyBoard.Disable();
            lightControlKeyBoard.Disable();
            lightControlKeyBoard.performed -= GetLightControl;
            sideBreakKeyBoard.Disable();
            sideBreakKeyBoard.performed -= GetSideBreakControl;
            GearChangeKeyBoard.Disable();
            GearChangeKeyBoard.performed -= GetGearChangeControl;
        }
        else
        {
            frontMoveSteeringWheel.Disable();
            leftRightSteeringWheel.Disable();
            breakSteeringWheel.Disable();
            lightControlSteeringWheel.Disable();
            lightControlSteeringWheel.performed -= GetLightControl;
            sideBreakSteeringWheel.Disable();
            sideBreakSteeringWheel.performed -= GetSideBreakControl;
            GearChangeSteeringWheel.Disable();
            GearChangeSteeringWheel.performed -= GetGearChangeControl;
        }
    }

    public float GetAccelerate(bool isKeyBoard)
    {
        if (isKeyBoard)
            return frontMoveKeyBoard.ReadValue<float>();
        else
            return frontMoveSteeringWheel.ReadValue<float>();
    }

    public float GetLeftRight(bool isKeyBoard)
    {
        if (isKeyBoard)
            return leftRightMoveKeyBoard.ReadValue<float>();
        else
            return leftRightSteeringWheel.ReadValue<float>();
    }

    public float GetBreak(bool isKeyBoard)
    {
        if (isKeyBoard)
            return breakKeyBoard.ReadValue<float>();
        else
            return breakSteeringWheel.ReadValue<float>();
    }

    public void GetLightControl(InputAction.CallbackContext context)
    {
        var control = context.control;
        
        switch(control.name)
        {
            case "l":
                lightController.ChangeFrontLightState();
                break;
            case "button4":
                lightController.ChangeFrontLightState();
                break;
            default:
                break;
        }
    }

    public void GetSideBreakControl(InputAction.CallbackContext context)
    {
        var control = context.control;

        switch (control.name)
        {
            case "0":
                wheelController.ToggleSideBrake();
                break;
            case "button16":
                wheelController.ToggleSideBrake();
                break;
            default:
                Debug.Log("Unknown input : " + control.name);
                break;
        }
    }

    public void GetGearChangeControl(InputAction.CallbackContext context)
    {
        var control = context.control;

        switch (control.name)
        {
            case "1":
                currentGear = D;
                Debug.Log("D");
                break;
            case "2":
                currentGear = N;
                Debug.Log("N");
                break;
            case "3":
                currentGear = R;
                Debug.Log("R");
                break;
            case "4":
                currentGear = P;
                Debug.Log("P");
                break;
            case "button17":
                currentGear = D;
                Debug.Log("D");
                break;
            case "button18":
                currentGear = N;
                Debug.Log("N");
                break;
            case "button21":
                currentGear = R;
                Debug.Log("R");
                break;
            case "button22":
                currentGear = P;
                Debug.Log("P");
                break;
            default:
                Debug.Log("Unknown input : " + control.name);
                break;
        }
    }
}
