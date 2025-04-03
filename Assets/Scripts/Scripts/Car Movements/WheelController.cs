using UnityEngine;

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

    [SerializeField] private float defaultAcceleration = 300f;
    [SerializeField] private float acceleration = 500f;
    [SerializeField] private float breakingForce = 500f;
    [SerializeField] private float maxTurnAngle = 15f;
    [SerializeField] private float noAccelerateMaxSpeed = 10f;

    private bool sideBreakOn = true;
    private float currentAcceleration = 0f;
    private float currentBrakingForce = 0f;
    private float currentTurnAngle = 0f;

    [Header("Car RigidBody")]

    [Space]

    [SerializeField] private Rigidbody CarBodyRb;

    public void ApplyAcceleration(float input, bool reverse)
    {
        float appliedInput = Mathf.Clamp01(input);

        currentAcceleration = acceleration * appliedInput;

        bool isLowSpeed = CarBodyRb.linearVelocity.magnitude * 3.6f < noAccelerateMaxSpeed;
        if (appliedInput == 0 && isLowSpeed)
        {
            currentAcceleration = defaultAcceleration;
            // Debug.Log("Applying default acceleration due to low speed.");
        }

        if (reverse)
        {
            currentAcceleration = -currentAcceleration;
        }

        UpdateMotorTorque();
    }

    public void ApplyWheelTurnAngle(float input)
    {
        currentTurnAngle = maxTurnAngle * input;
        UpdateSteerAngle();
    }

    public void ToggleSideBrake()
    {
        sideBreakOn = !sideBreakOn;
    }

    public void ApplyBrakeForce(float input)
    {
        currentBrakingForce = breakingForce * Mathf.Max(0, input);
        if (sideBreakOn) currentBrakingForce = breakingForce;

        UpdateBrakingTorque();
    }

    private void UpdateMotorTorque()
    {
        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;
    }

    private void UpdateSteerAngle()
    {
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
    }

    private void UpdateBrakingTorque()
    {
        frontLeft.brakeTorque = currentBrakingForce;
        frontRight.brakeTorque = currentBrakingForce;
        backLeft.brakeTorque = currentBrakingForce;
        backRight.brakeTorque = currentBrakingForce;
    }
}
