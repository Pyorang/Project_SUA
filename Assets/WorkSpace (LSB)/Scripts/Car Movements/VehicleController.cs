using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    //private Gear currentGear;

    [SerializeField] private LightController lightController;
    [SerializeField] private WheelController wheelController;
    [SerializeField] private SteeringWheelController steeringWheelController;

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
        wheelController.ApplyAcceleration(GetAccelerate());
        wheelController.ApplyWheelTurnAngle(GetLeftRight());
        wheelController.ApplyBreakForce(GetBreak());

        steeringWheelController.UpdateSteeringWheel(GetLeftRight());
    }

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
        GearChangeAction?.Disable();
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
            case "numpad0":
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
            case "numpad1":
                
                break;
            case "numpad2":
                
                break;
            case "numpad3":
                
                break;
            case "numpad4":
                
                break;
            default:
                break;
        }
    }
}
