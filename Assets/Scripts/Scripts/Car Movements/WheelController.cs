using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;

public class WheelController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    
    [Space]

    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider frontRight;
    
    [Space]

    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;

    [Header("Wheel Transforms")]

    [Space]

    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform frontRightTransform;
    
    [Space]

    [SerializeField] Transform backLeftTransform;
    [SerializeField] Transform backRightTransform;

    [Header("Car Settings")]
    
    [Space]

    [SerializeField] private float acceleration = 500f;
    [SerializeField] private float breakingForce = 500f;
    [SerializeField] private float maxTurnAngle = 15f;

    private bool sideBreakOn = true;
    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    public void ApplyAcceleration(float Input)
    {
        if (Input < 0)
            Input = 0;
        currentAcceleration = acceleration * Input;
        ChangeFrontWheelsMotorTorque();
    }

    public void ApplyWheelTurnAngle(float Input)
    {
        currentTurnAngle = maxTurnAngle * Input;
        ChangeFrontWheelsSteerAngle();
        UpdateAllWheelsTrnasform();
    }

    public void ApplySideBreak()
    {
        sideBreakOn = !sideBreakOn;
    }

    public void  ApplyBreakForce(float Input)
    {
        if (Input < 0)
            Input = 0;

        currentBreakForce = breakingForce * Input;
        
        if(sideBreakOn)
            currentBreakForce = breakingForce;

        ChangeAllWheelsBreakTorque();
    }

    private void ChangeFrontWheelsMotorTorque()
    {
        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;
    }

    public void ChangeFrontWheelsSteerAngle()
    {
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
    }

    public void UpdateAllWheelsTrnasform()
    {
        UpdateWheelTransform(frontLeft, frontLeftTransform);
        UpdateWheelTransform(frontRight, frontRightTransform);
        UpdateWheelTransform(backLeft, backLeftTransform);
        UpdateWheelTransform(backRight, backRightTransform);
    }

    public void UpdateWheelTransform(WheelCollider col, Transform trans)
    {
        /*Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;*/
    }

    private void ChangeAllWheelsBreakTorque()
    {
        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
    }
}
