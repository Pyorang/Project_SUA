using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    [Header("Controllers")]
    [Space]

    [SerializeField] private WheelController wheelController;
    [SerializeField] private SteeringWheelController steeringWheelController;
    [SerializeField] private LightController lightController;

    #region Gears
    private Gear D, N, R, P;
    private Gear currentGear;
    #endregion
    #region InputSystems
    KeyBoardInputActions action;
    InputAction moveFrontBackAction;
    InputAction moveLeftRightAction;
    InputAction breakAction;
    InputAction lightControlAction;
    InputAction sideBreakAction;
    InputAction GearChangeAction;
    #endregion

    private void Awake()
    {
        AllocateInputActions();
        InitGears();
    }

    private void OnEnable()
    {
        EnableAllActions();
    }

    private void OnDisable()
    {
        DisableAllActions();
    }

    private void FixedUpdate()
    {
        currentGear.Drive();
    }

    public WheelController GetWheelController() { return wheelController; }
    public SteeringWheelController GetSteeringWheelController() { return steeringWheelController; } 

    void AllocateInputActions()
    {
        action = new KeyBoardInputActions();
        moveFrontBackAction = action.Car.FrontBack;
        moveLeftRightAction = action.Car.LeftRight;
        breakAction = action.Car.Break;
        lightControlAction = action.Car.LightControl;
        sideBreakAction = action.Car.SideBreak;
        GearChangeAction = action.Car.GearChange;
    }

    void InitGears()
    {
        D = new DriveGear(this);
        N = new NeutralGear(this);
        R = new ReverseGear(this);
        P = new ParkingGear(this);
        currentGear = P;
    }

    void EnableAllActions()
    {
        moveFrontBackAction.Enable();
        moveLeftRightAction.Enable();
        breakAction.Enable();
        lightControlAction.Enable();
        lightControlAction.performed += GetLightControl;
        sideBreakAction.Enable();
        sideBreakAction.performed += GetSideBreakControl;
        GearChangeAction.Enable();
        GearChangeAction.performed += GetGearChangeControl;
    }

    void DisableAllActions()
    {
        moveFrontBackAction.Disable();
        moveLeftRightAction.Disable();
        breakAction.Disable();
        lightControlAction.Disable();
        lightControlAction.performed -= GetLightControl;
        sideBreakAction.Disable();
        sideBreakAction.performed -= GetSideBreakControl;
        GearChangeAction.Disable();
        GearChangeAction.performed -= GetGearChangeControl;
    }

    public float GetAccelerate()
    {
        return moveFrontBackAction.ReadValue<float>();
    }

    public float GetLeftRight()
    {
        return moveLeftRightAction.ReadValue<float>();
    }

    public float GetBreak()
    {
        return breakAction.ReadValue<float>();
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
                wheelController.ApplySideBreak();
                break;
            case "button16":
                wheelController.ApplySideBreak();
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
