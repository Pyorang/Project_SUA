using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{

    [SerializeField] private LightController lightController;
    [SerializeField] private WheelController wheelController;
    [SerializeField] private SteeringWheelController steeringWheelController;

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
        wheelController.ApplyBreakForce(false);  // 처음 사이드 브레이크 작동
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

    public bool GetBreak()
    {
        return breakAction.phase == InputActionPhase.Performed;
    }

    public void GetLightControl(InputAction.CallbackContext context)
    {
        var control = context.control;
        
        switch(control.name)
        {
            case "j":
                lightController.ChangeLeftRightSignal(LeftRIghtSignal.Left);
                break;
            case "k":
                lightController.ChangeLeftRightSignal(LeftRIghtSignal.Middle);
                break;
            case "l":
                lightController.ChangeLeftRightSignal(LeftRIghtSignal.Right);
                break;
            case "r":
                lightController.ChangeEmergencyLights();
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
            default:
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
            default:
                break;
        }
    }
}
