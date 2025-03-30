using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    [Header("Steering Wheel Sensitivity Settings")]
    
    public float maxTurnAngle = 135f;
    public float turnSpeed = 20.0f;

    [Header("Steering Wheel Transform")]
    
    [SerializeField] Transform steeringWheel;

    private float currentTurnAngle = 0f;

    public void UpdateSteeringWheel(float Input)
    {
        Vector3 rotation = steeringWheel.localEulerAngles;

        float targetTurnAngle = maxTurnAngle * Input;

        rotation.z = -targetTurnAngle;
        steeringWheel.localRotation = Quaternion.Euler(rotation);
    }
}
