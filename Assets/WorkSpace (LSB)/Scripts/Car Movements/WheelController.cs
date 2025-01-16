using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class WheelController : MonoBehaviour
{
    //각도 Lerp 이용하기

    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;

    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform backLeftTransform;
    [SerializeField] Transform backRightTransform;

    public bool sideBreakOn = true;

    public float acceleration = 500f;
    public float breakingForce = 500f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    public void ApplyAcceleration(float Input)
    {
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

    public void  ApplyBreakForce(bool Input)
    {
        if(sideBreakOn || Input)
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            if (!Input)
                currentBreakForce = 0f;
        }

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
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }

    private void ChangeAllWheelsBreakTorque()
    {
        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
    }
}
