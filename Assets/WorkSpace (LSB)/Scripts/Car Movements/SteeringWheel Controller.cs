using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public float maxTurnAngle = 60f;
    public float turnSpeed = 4.0f;
    private float currentTurnAngle = 0f;

    [SerializeField] Transform steeringWheel;

    public void UpdateSteeringWheel(float Input)
    {
        Vector3 rotation = steeringWheel.localEulerAngles;

        float targetTurnAngle = maxTurnAngle * Input;

        currentTurnAngle = Mathf.Lerp(currentTurnAngle, targetTurnAngle, Time.deltaTime * turnSpeed);

        rotation.z = -currentTurnAngle;
        steeringWheel.localRotation = Quaternion.Euler(rotation);
    }
}
